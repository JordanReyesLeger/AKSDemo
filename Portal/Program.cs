using Azure.Identity;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Configurar Key Vault si está habilitado
            var keyVaultUri = builder.Configuration["KeyVault:Uri"];
            bool useIdentity = bool.Parse(builder.Configuration["UseIdentity"]);

            if (useIdentity)
            {
                // Settings con Identity
                if (!string.IsNullOrEmpty(keyVaultUri))
                {
                    builder.Configuration.AddAzureKeyVault(
                        new Uri(keyVaultUri),
                        new DefaultAzureCredential());
                }
            }
            else
            {
                // Configurar Key Vault utilizando Client ID y Client Secret
                var keyVaultConfig = builder.Configuration.GetSection("AzureKeyVault");
                var clientId = keyVaultConfig["ClientId"];
                var clientSecret = keyVaultConfig["ClientSecret"];
                var tenantId = keyVaultConfig["TenantId"];

                if (!string.IsNullOrEmpty(keyVaultUri) && !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret) && !string.IsNullOrEmpty(tenantId))
                {
                    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), credential);
                }
            }


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pasteles}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
