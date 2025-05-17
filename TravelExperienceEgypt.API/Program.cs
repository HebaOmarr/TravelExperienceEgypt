
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelExperienceEgypt.DataAccess.Data;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.UnitOfWork;
using TravelExperienceEgypt.BusinessLogic.Services;
using TravelExperienceEgypt.API.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TravelExperienceEgypt.DataAccess.Repository.Contract;
using TravelExperienceEgypt.DataAccess.Repository;

namespace TravelExperienceEgypt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDBContext>(
              options =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConnectionString"));
              }
              );

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPostRepo, PostRepo>();
            builder.Services.AddScoped<IPlaceRepo, PlaceRepo>();
            builder.Services.AddScoped<AccountService>();
            builder.Services.AddScoped<IWishlistRepo,WishlistRepo>();


            /*  builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<ApplicationRole>()
                  .AddEntityFrameworkStores<ApplicationDBContext>()
                  .AddDefaultTokenProviders();
  */

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
           .AddEntityFrameworkStores<ApplicationDBContext>()
           .AddDefaultTokenProviders();



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            //JWt
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme; 
                options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });

            //Swagger
            #region Swagget Setting
            //   builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });
            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
