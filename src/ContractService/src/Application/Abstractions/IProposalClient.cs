namespace Application.Abstractions;

public interface IProposalClient
{
    Task<bool> IsProposalApprovedAsync(Guid proposalId, CancellationToken cancellationToken = default);
}
