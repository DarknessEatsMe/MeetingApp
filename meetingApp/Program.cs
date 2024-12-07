using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace meetingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );  
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(
                    options =>
                    {
                        options.LoginPath = "/User/LoginPage";
                        options.ExpireTimeSpan = TimeSpan.FromDays(1);
                        options.Cookie.MaxAge = options.ExpireTimeSpan;
                        options.SlidingExpiration = true;
                    });

            builder.Services.AddAuthorization();
            builder.Services.AddSession();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var app = builder.Build();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapGet("/Home/Exit", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/");
            });

            app.Run();
        }
    }
}