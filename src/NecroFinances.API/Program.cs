
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using NecroFinances.Application.Services;
using NecroFinances.Infrastructure.Persistence;
using NecroFinances.Infrastructure.Repositories;
using System.Text;

namespace NecroFinances.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<ISettingsService, SettingsService>();
            builder.Services.AddScoped<ISettingsRepositorie, SettingsRepositorie>();
            builder.Services.AddScoped<IMesRepositorie, MesRepositorie>();
            builder.Services.AddScoped<IPatrimonioRepositorie, PatrimonioRepositorie>();
            builder.Services.AddScoped<IUserRepositorie, UserRepositorie>();

            builder.Services.AddScoped<IGastosService, GastosService>();
            builder.Services.AddScoped<IGastosRepositorie, GastosRepositorie>();
            builder.Services.AddScoped<IMesService, MesService>();
            builder.Services.AddScoped<IPatrimonioService, PatrimonioService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var cs = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(cs, new MySqlServerVersion(new Version(8, 0, 36)));
            });

            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("Jwt")
            );

            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .WithOrigins("http://192.168.70.6:4200", "http://localhost:4200", "http://necro.local")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
            {
                var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.TokenKey)
                    ),

                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
