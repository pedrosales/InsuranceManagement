using Application.Abstractions;
using MediatR;

namespace Application.Contract.Proposal;

internal sealed class ContractProposalCommandHandler : IRequestHandler<ContractProposalCommand>
{
    private readonly IProposalClient _client;
    private readonly IEventPublisher _eventPublisher;

    public ContractProposalCommandHandler(IProposalClient proposalClient, IEventPublisher eventPublisher)
    {
       _client = proposalClient;
       _eventPublisher = eventPublisher;
    }

    public async Task Handle(ContractProposalCommand request, CancellationToken cancellationToken)
    {
        bool result = await _client.IsProposalApprovedAsync(request.ProposalId, cancellationToken);

        if (result)
        {
            await _eventPublisher.PublishContractProposalAsync(request.ProposalId, cancellationToken);
        }
    }
}
