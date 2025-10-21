using Application.Abstractions;
using MassTransit;
using SharedKernel;

namespace Infrastructure.Messaging;

public sealed class ContractProposalEventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public async Task PublishContractProposalAsync(Guid proposalId, CancellationToken cancellationToken = default)
    {
        await publishEndpoint.Publish(new ContractProposalCreatedEvent(proposalId), cancellationToken);
    }
}
