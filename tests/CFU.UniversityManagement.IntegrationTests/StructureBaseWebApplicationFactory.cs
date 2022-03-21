using CFU.UniversityManagement.Infrastructure;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CFU.UniversityManagement.IntegrationTests;

class StructureBaseWebApplicationFactory : WebApplicationFactory<Program>
{
    private ICompositeService _compositeService = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var config = GetConfiguration();

        var dockerComposeFileNames = config["DockerComposeFileNames"].Split(';');
        var dockerComposeFiles = dockerComposeFileNames.Select(f => GetDockerComposeLocation(f)).ToArray();

        _compositeService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(dockerComposeFiles)
            .RemoveOrphans()
            .Build().Start();

        builder.UseEnvironment("Testing");

        builder.UseConfiguration(config);

        builder.ConfigureServices(services => {
            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UniversityManagementDBContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<StructureBaseWebApplicationFactory>>();

            context.Database.EnsureCreated();

            var str = context.Database.GetDbConnection().ConnectionString;

            try {
                context.Inititalize();
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the " +
                    "database with test messages. Error: {Message}", ex.Message);
            }
        });
    }

    public override ValueTask DisposeAsync()
    {
        _compositeService.Stop();
        _compositeService.Dispose();
        return base.DisposeAsync();
    }

    private static IConfiguration GetConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

    private static string GetDockerComposeLocation(string dockerComposeFileName)
    {
        var directory = Directory.GetCurrentDirectory();
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName))) {
            directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];
        }
        return Path.Combine(directory, dockerComposeFileName);
    }
}
