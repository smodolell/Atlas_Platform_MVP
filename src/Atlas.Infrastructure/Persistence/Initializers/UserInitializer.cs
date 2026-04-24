using Atlas.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Atlas.Infrastructure.Persistence.Initializers;

internal class UserInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public UserInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Rol>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();

            string webmasterRoleName = "Webmaster";
            bool adminRoleExists = await roleManager.RoleExistsAsync(webmasterRoleName);

            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new Rol(webmasterRoleName));
            }

            string adminEmail = "sergio.modolell@gmail.com";
            string adminPassword = "Sergio123456%"; // En producción usa una contraseña segura

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new Usuario
                {
                    NombreCompleto = "Sergio Modolell",
                    Telefono = "0387-4096276",
                    FechaRegistro = DateTime.Now,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = false // Puedes establecer esto como true si no necesitas confirmación
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);

                if (createResult.Succeeded)
                {
                    // Asignar rol al usuario
                    await userManager.AddToRoleAsync(adminUser, webmasterRoleName);
                }
                else
                {
                    // Manejar errores de creación
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    throw new Exception($"No se pudo crear el usuario administrador: {errors}");
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
