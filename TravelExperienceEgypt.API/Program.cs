
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelExperienceEgypt.DataAccess.Data;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.UnitOfWork;

using TravelExperienceEgypt.API.DTOs;

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


            builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();


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
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
