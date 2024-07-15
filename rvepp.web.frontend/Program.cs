using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using rvepp.web.frontend.Configuration;
using rvepp.web.frontend.Database;
using System.Text;

namespace rvepp.web.frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();
            var apiConfig = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();

            if (apiConfig is not null)
            {
                builder.Services.AddSingleton(apiConfig);

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = apiConfig?.JWTIssuer,
                        ValidAudience = apiConfig?.JWTAudience,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiConfig.JWTSecret))
                    };
                });
            }

            builder.Services.AddRazorPages();

            var configuration = builder.Configuration.GetSection("RveppDbContext");

            if (configuration.Value is null)
            {
                throw new Exception("Configuration not set");
            }

            builder.Services.AddControllersWithViews();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<RveppDbContext>(options => options.UseNpgsql(configuration.Value));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            } else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RveppDbContext>();

                db.Database.Migrate();
            }

            app.Run();
        }
    }
}