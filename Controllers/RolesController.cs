using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Managment_API.Model;
using System.Data;

namespace Project_Managment_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return BadRequest("Role already exists.");
            }

            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' created successfully.");
            }

            return StatusCode(500, "Failed to create role.");
        }

        [HttpPut("{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(string roleId, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            role.Name = newRoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok($"Role '{newRoleName}' updated successfully.");
            }

            return StatusCode(500, "Failed to update role.");
        }

        [HttpDelete("{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role deleted successfully.");
            }

            return StatusCode(500, "Failed to delete role.");
        }

        [HttpPost("users/{userId}/roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser(string roleName, string userId)
        {
            var role = await _roleManager.RoleExistsAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var isInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (isInRole)
            {
                return BadRequest("User already has the role.");
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' added to the user successfully.");
            }

            return StatusCode(500, "Failed to add role to user.");
        }
        [HttpDelete("{roleName}/users/{userId}")]
        public async Task<IActionResult> RemoveUserFromRole(string roleName, string userId)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok($"User '{user.UserName}' removed from role '{roleName}' successfully.");
            }

            return BadRequest("Failed to remove user from role.");
        }

    }
}
       