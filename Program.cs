using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_Management_API.Data;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using Project_Managment_API.Repository;
using System;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;
using Task = System.Threading.Tasks.Task;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var jwtSecret = Environment.GetEnvironmentVariable("JwtSecret");
        var conString = Environment.GetEnvironmentVariable("DefaultConnection");
        string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
        var serviceSid = Environment.GetEnvironmentVariable("Service_Sid");

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddDbContext<ProjectManagementDbContext>(options =>
            options.UseMySQL(conString));
        builder.Services.AddScoped<IApplicationUserRepository, UserRepository>();
        builder.Services.AddScoped<ITenantRepository, TenantRepository>();
        builder.Services.AddScoped<IOtpRepository, OtpRepository>();

        builder.Services.AddSignalR();



        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ProjectManagementDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.AddScoped<RoleManager<IdentityRole>>();
        builder.Services.AddScoped<UserManager<ApplicationUser>>();

        builder.Services.AddAuthentication(b =>
        {
            b.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            b.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            b.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                ValidIssuer = "Pm",
                ValidAudience = "PMUsers",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
            };

        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
