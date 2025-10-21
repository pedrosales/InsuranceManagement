namespace Application.Abstractions;

public interface IEventPublisher
{
    Task PublishContractProposalAsync(Guid proposalId, CancellationToken cancellationToken = default);
}
