using Microsoft.EntityFrameworkCore;

using rvepp.web.frontend.Database;

namespace rvepp.web.frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddRazorPages();

            var configuration = builder.Configuration.GetSection("RveppDbContext");

            if (configuration.Value is null)
            {
                throw new Exception("Configuration not set");
            }

            builder.Services.AddDbContext<RveppDbContext>(options => options.UseNpgsql(configuration.Value));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

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