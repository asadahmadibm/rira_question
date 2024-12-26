using FluentValidation;
using FluentValidation.AspNetCore;
using GrpcCrudExample.Data;
using GrpcCrudExample.Middleware;
using GrpcCrudExample.Middlewares;
using GrpcCrudExample.Repositories;
using GrpcCrudExample.Services;
using GrpcCrudExample.Validators;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace GrpcCrudExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddDbContext<PersonDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();

            // پیکربندی Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<PersonValidator>();
            // پیکربندی Kestrel
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(3322, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2; // تنظیم پروتکل HTTP/2
                });
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<PersonsService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}