using MediatR;

namespace Application.Proposal.Get;

public sealed record GetProposalsQuery() : IRequest<List<ProposalResponse>>;
