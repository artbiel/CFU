using CFU.Domain.StructureContext.AcademyAggregate;
using CFU.Domain.StructureContext.DepartmentAggregate;
using CFU.Domain.StructureContext.FacultyAggregate;
using CFU.Domain.StructureContext.InstituteAggregate;
using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.UniversityManagement.Application.Common.Behaviors;
using CFU.UniversityManagement.Infrastructure.Configuration;
using CFU.UniversityManagement.Infrastructure.Structure.Repositories;
using CFU.UniversityManagement.Infrastructure.Supply.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CFU.UniversityManagement.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDB(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDbContext<UniversityManagementDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => {
                    b.MigrationsAssembly(typeof(Program).Assembly.FullName);
                    b.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), default!);
                }))
            .AddScoped<IAcademyRepository, AcademyRepository>()
            .AddScoped<IInstituteRepository, InstituteRepository>()
            .AddScoped<IFacultyRepository, FacultyRepository>()
            .AddScoped<IDepartmentRepository, DepartmentRepository>()
            .AddScoped<IBuildingRepository, BuildingRepository>()
            .AddScoped<ITransactionManager, TransactionManager>()
            .Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));

    public static IServiceCollection AddMediator(this IServiceCollection services)
       => services
            .AddMediatR(typeof(ICommand<>), typeof(UniversityManagementDBContext))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalBehavior<,>));

    public static IServiceCollection AddDomainServices(this IServiceCollection services) =>
        services
            .AddScoped<AcademyFactory>()
            .AddScoped<InstituteFactory>()
            .AddScoped<BuildingFactory>();


    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        hcBuilder.AddSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                name: "UniversityManagementDB-check",
                tags: new string[] { "universitymanagementdb" });

        if (environment.IsProduction()) {
            hcBuilder.AddRedis(
                    configuration.GetConnectionString("Cache"),
                    name: "UniversityManagementCache-check",
                    tags: new string[] { "universitymanagementcache" });
        }

        return services;
    }
}
