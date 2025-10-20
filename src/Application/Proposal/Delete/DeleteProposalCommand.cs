using MediatR;

namespace Application.Proposal.Delete;

public sealed record DeleteProposalCommand(Guid ProposalId) : IRequest<bool>;
