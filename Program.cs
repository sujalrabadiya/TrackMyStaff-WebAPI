using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;
using TrackMyStaffWebApplication.Data;

namespace TrackMyStaffWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddFluentValidation(c =>
                c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<AuthRepository>();
            builder.Services.AddScoped<LocationRepository>();
            builder.Services.AddScoped<MessageRepository>();
            builder.Services.AddScoped<NotificationRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<CompanyRepository>();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin() 
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()); 
            });

            var app = builder.Build();

            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
