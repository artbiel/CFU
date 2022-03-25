using CFU.UniversityManagement.WebAPI.Extensions;
using CFU.UniversityManagement.WebAPI.Middlewares;
using FastEndpoints.Swagger;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.AddCustomHealthChecks(builder.Configuration, builder.Environment);

builder.Services.AddMediator();

builder.Services.AddDB(builder.Configuration);
builder.Services.AddDomainServices();

builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(IQuery<>)));

builder.Services.AddAutoMapper(typeof(UniversityManagementDBContext));

if (!builder.Environment.IsProduction()) {
    builder.Services.AddDistributedMemoryCache();
}
else {
    builder.Services.AddStackExchangeRedisCache(options => {
        options.Configuration = builder.Configuration.GetConnectionString("Cache");
    });
}

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseRouting();

app.UseFastEndpoints();

app.UseEndpoints(endpoints => {
    endpoints.MapHealthChecks("/health");
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.MigrateDatabase<UniversityManagementDBContext>();

app.Run();

