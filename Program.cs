
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using FluentValidation;
using WebApiCar.Application.Services;
using WebApiCar.Infrastructure.ConnectionStrings;
using WebApiCar.Infrastructure.DatabseContexts;
using WebApiCar.Infrastructure.Middlewares;
using WebApiCar.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApiCar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<PostgresSqlConnectionString>(
                builder.Configuration.GetSection(nameof(PostgresSqlConnectionString)));

            builder.Services.AddTransient<PostgresSqlConnectionString>();
            builder.Services.AddDbContext<CarDbContext>(
                (sp, o) =>
                {
                    var connectionString =
                        sp.GetRequiredService<PostgresSqlConnectionString>().Value;
                    o.UseNpgsql(connectionString);
                }
            );

            builder.Services.AddScoped<ExceptionMiddleware>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "carApi.Authentication";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.Cookie.IsEssential = true;
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}