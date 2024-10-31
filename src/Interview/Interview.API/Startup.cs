using System.Text.Json.Serialization;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using Interview.Application.Mappings;
using Interview.Domain.Interfaces;
using Interview.Infrastructure.Repositories;
using Interview.Infrastructure.Data;
using Interview.Application.Validators;
using Interview.Application.Features.Queries.Employee;
using Interview.Application.DTOs;
using Interview.Application.Models;
using Interview.Application.Features.Handlers.Employee.QueryHandlers;
using Interview.Application.Features.Queries.Outlet;
using Interview.Application.Features.Handlers.Outlet.QueryHandlers;

namespace Interview.API;

public class Startup
{
    public IConfiguration Configuration { get; }
    private string allowedSpecificOrigins = "AllowSpecificOrigins";
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
    {
        Console.Title = System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Name ?? "Interview API";
        services.AddControllers(options => options.EnableEndpointRouting = false)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddMediatR(typeof(Startup).Assembly);
        services.AddAutoMapper(typeof(MappingProfile));
        //MyCors(services);
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        services.AddTransient<IRequestHandler<GetEmployeeByCodeQuery, ResponseModel<EmployeeDto>>, GetEmployeeByCodeQueryHandler>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IRequestHandler<GetOutletByCodeQuery, ResponseModel<OutletDto>>, GetOutletByCodeQueryHandler>();
        services.AddScoped<IOutletRepository, OutletRepository>();
        services.AddTransient<IRequestHandler<GetEmployeeAttendanceInformationQuery, ResponseModel<List<EmployeeWorkTimeDto>>>, GetEmployeeAttendanceInformationQueryHandler>();
        services.AddSingleton<IUnitOfWork>(provider => new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddValidators();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseCors(allowedSpecificOrigins);
        app.UseCors("AllowAll");
        app.UseResponseCompression();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private void MyCors(IServiceCollection services)
    {
        string[]? allowedOrigins = Configuration
                 .GetSection("AllowedOrigins")
                 .Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy(allowedSpecificOrigins, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .SetPreflightMaxAge(TimeSpan.FromDays(1))
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });
    }
}
