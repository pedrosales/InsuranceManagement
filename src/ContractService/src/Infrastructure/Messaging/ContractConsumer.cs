using MassTransit;
using SharedKernel;

namespace Infrastructure.Messaging;

public sealed class ContractConsumer : IConsumer<ContractProposalCreatedEvent>
{
    public Task Consume(ConsumeContext<ContractProposalCreatedEvent> context)
    {
        ContractProposalCreatedEvent evento = context.Message;
        
        return Task.CompletedTask;
    }
}
