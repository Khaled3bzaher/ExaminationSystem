
using Domain.Contracts;
using ExaminationSystem.Web.Factories;
using ExaminationSystem.Web.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;

namespace ExaminationSystem.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            //Register My Services
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            

            var app = builder.Build();
            await app.InitializeDatabaseAsync();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        
    }
}
