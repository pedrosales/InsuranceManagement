using MassTransit;
using SharedKernel;

namespace Application.Contract.Consumer;

public sealed class ContractConsumer : IConsumer<ContractProposalCreatedEvent>
{
    public Task Consume(ConsumeContext<ContractProposalCreatedEvent> context)
    {
        ContractProposalCreatedEvent evento = context.Message;
        
        return Task.CompletedTask;
    }
}
