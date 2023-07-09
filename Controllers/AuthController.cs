using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project_Managment_API.DTO;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TODO.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly string _jwtSecret;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOtpRepository _otpRepository;

        public AuthController(IApplicationUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOtpRepository otpRepository)
        {
            _userRepository = userRepository;
            _jwtSecret = Environment.GetEnvironmentVariable("JwtSecret");
            _userManager = userManager;
            _roleManager = roleManager;
            _otpRepository = otpRepository;
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            TwilioClient.Init(accountSid, authToken);
            

        }
        [HttpPost("reset-password-sms")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userRepository.GetUserByEmail(resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Check if the entered phone number matches the user's stored phone number
            if (user.PhoneNumber != resetPasswordDto.PhoneNumber)
            {
                return BadRequest("Phone number does not match any user in our database.");
            }

            var otp = GenerateOTP();

            var otpModel = new OtpModel
            {
                Email = resetPasswordDto.Email,
                Otp = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5)
            };

            await _otpRepository.AddOtpAsync(otpModel);

            // Send OTP via SMS using Twilio
            var message = $"Your OTP is: {otp}";

            try
            {
                var smsMessage = await MessageResource.CreateAsync(
                    body: message,
                    from: new Twilio.Types.PhoneNumber("+14178073763"),
                    to: new Twilio.Types.PhoneNumber(resetPasswordDto.PhoneNumber)
                );

                return Ok("OTP has been sent for verification.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to send OTP.");
            }
        }

        [HttpPut("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP(VerifyOtpDto verifyOtpDto)
        {
            var storedOtpModel = await _otpRepository.GetOtpByEmailAsync(verifyOtpDto.Email);

            if (storedOtpModel == null || storedOtpModel.Otp != verifyOtpDto.Otp)
            {
                return BadRequest("Invalid OTP.");
            }

            if (DateTime.UtcNow > storedOtpModel.ExpirationTime)
            {
                return BadRequest("OTP has expired.");
            }

            var user = await _userRepository.GetUserByEmail(verifyOtpDto.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, storedOtpModel.Otp, verifyOtpDto.NewPassword);

            if (!resetPasswordResult.Succeeded)
            {
                var errorMessage = string.Join(", ", resetPasswordResult.Errors.Select(error => error.Description));
                return BadRequest($"Failed to update password. Errors: {errorMessage}");
            }

            await _otpRepository.DeleteOtpModelAsync(storedOtpModel);

            return Ok("Password has been successfully updated.");
        }



        private string GenerateOTP()
        {
            Random random = new Random();
            const string chars = "0123456789";
            string otp = "";

            for (int i = 0; i < 6; i++)
            {
                otp += chars[random.Next(chars.Length)];
            }

            return otp;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (await _userRepository.UserExists(userDto.Email))
            {
                return BadRequest("Username already exists.");
            }

            if (userDto.ConfirmPassword != userDto.Password)
            {
                return BadRequest("Passwords do not match.");
            }
            var newTenant = new Tenant
            {
                
                Name = userDto.TenantName
            };

            // Create the new user
            var newUser = new ApplicationUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                Tenant = newTenant, // Associate the user with the new tenant
                PhoneNumber = userDto.PhoneNumber
            };

            var createdUser = await _userRepository.CreateUser(newUser, userDto.Password);

            // Assign appropriate roles to the user
            await _userManager.AddToRoleAsync(newUser, "Admin");

            // Generate JWT token
            var userRoles = await _userManager.GetRolesAsync(createdUser);
            var token = GenerateJwtToken(createdUser, newUser.Tenant.Id.ToString(), userRoles);

            return Ok(new { token });
        }
        [HttpPost("register/same-tenant")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RegisterToSameTenant(UserSameTenantDto userDto)
        {
            if (await _userRepository.UserExists(userDto.Email))
            {
                return BadRequest("Username already exists.");
            }

            if (userDto.ConfirmPassword != userDto.Password)
            {
                return BadRequest("Passwords do not match.");
            }

            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var currentUser = await _userManager.Users
          .Include(u => u.Tenant)
          .FirstOrDefaultAsync(u => u.Email == currentUserId);

            ;

            if (currentUser == null)
            {
                return Unauthorized("Invalid user.");
            }

            var newTenant = currentUser.Tenant; // Get the current user's tenant

            if (newTenant == null)
            {
                return BadRequest("Invalid tenant.");
            }

            var newUser = new ApplicationUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                TenantId = newTenant.Id ,
                PhoneNumber = userDto.PhoneNumber
            };

            var createdUser = await _userRepository.CreateUser(newUser, userDto.Password);
            await _userManager.AddToRoleAsync(newUser, "Member");

            var userRoles = await _userManager.GetRolesAsync(createdUser);
            var token = GenerateJwtToken(createdUser, newUser.TenantId.ToString(), userRoles);

            return Ok(new { token });
        }



        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            var user = await _userRepository.GetUserByEmail(userDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var passwordIsValid = await _userRepository.CheckPassword(user, userDto.Password);

            if (!passwordIsValid)
            {
                return Unauthorized("Invalid username or password.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, user.TenantId.ToString(), userRoles);

            return Ok(new { token });
        }

        private string GenerateJwtToken(ApplicationUser user, string tenantId, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("TenantId", tenantId),
                    // Add roles as claims
                    new Claim(ClaimTypes.Role, string.Join(",", roles))
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
