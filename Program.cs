using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HygeeaMind.Data;
using HygeeaMind.Hubs; // Ad?ug?m acest using pentru a putea referi ChatHub

namespace HygeeaMind;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Ad?ugarea serviciilor la container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        // Ad?ug?m serviciul SignalR la containerul de servicii.
        // Acest lucru este necesar pentru a permite func?ionalitatea de chat live.
        builder.Services.AddSignalR();

        var app = builder.Build();

        // Configurarea pipeline-ului de cereri HTTP.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // Valoarea implicit? HSTS este 30 de zile. Po?i modifica asta pentru scenarii de produc?ie.
            // Vezi https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection(); // Redirec?ioneaz? cererile HTTP c?tre HTTPS
        app.UseStaticFiles();     // Activeaz? servirea fi?ierelor statice (CSS, JS, imagini, PDF)

        app.UseRouting();         // Activeaz? rutarea

        app.UseAuthentication();  // Activeaz? autentificarea utilizatorilor
        app.UseAuthorization();   // Activeaz? autorizarea utilizatorilor

        // Mapeaz? rutele pentru controalele MVC.
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Mapeaz? paginile Razor (utilizate de ASP.NET Identity pentru login/register).
        app.MapRazorPages();

        // Mapeaz? hub-ul SignalR la o rut? URL.
        // To?i clien?ii care vor s? comunice cu chat-ul se vor conecta la aceast? rut?.
        app.MapHub<ChatHub>("/chatHub");

        app.Run(); // Porneste aplica?ia web.
    }
}