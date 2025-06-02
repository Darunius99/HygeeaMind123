using HygeeaMind.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Necesare pentru EnsureCreatedAsync

namespace HygeeaMind.Utilities
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Obține instanțe ale UserManager și RoleManager din ServiceProvider
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Asigură-te că baza de date este creată și migrată (dacă nu s-a întâmplat deja)
            // Acest lucru este important pentru scenariile de inițializare
            await context.Database.MigrateAsync(); // Aplică migrațiile la startup

            string adminRoleName = "Admin";
            string userRoleName = "User";

            // Creează rolul "Admin" dacă nu există
            if (await roleManager.FindByNameAsync(adminRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            }

            // Creează rolul "User" dacă nu există
            if (await roleManager.FindByNameAsync(userRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRoleName));
            }

            // Creează un utilizator "admin" și atribuie-i rolul "Admin"
            string adminEmail = "admin@hygeeamind.com"; // Folosește o adresă de email reală pentru admin
            string adminPassword = "Password123!"; // Parolă puternică și complexă, NU folosi asta în producție!

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
                else
                {
                    // Afișează erorile dacă crearea utilizatorului eșuează
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Eroare creare admin: {error.Description}");
                    }
                }
            }
        }
    }
}