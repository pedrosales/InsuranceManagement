using Application.Abstractions;
using Infrastructure.Clients;
using Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services.AddEvents();

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
