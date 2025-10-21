using Application.Abstractions;
using Application.Abstractions.Data;
using Infrastructure.Clients;
using Infrastructure.Database;
using Infrastructure.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration) =>
        services
        .AddDatabase(configuration)
        .AddEvents();

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ContractConsumer>();

            x.UsingInMemory((context, cfg) =>
            cfg.ConfigureEndpoints(context));
        });



        services.AddScoped<IEventPublisher, ContractProposalEventPublisher>();

        return services;
    }
}
