using MediatR;

namespace Application.Proposal.GetById;

public sealed record GetProposalByIdQuery(Guid ProposalId) : IRequest<ProposalResponse>;
