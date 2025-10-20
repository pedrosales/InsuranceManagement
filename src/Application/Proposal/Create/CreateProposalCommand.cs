using MediatR;

namespace Application.Proposal.Create;

public sealed record CreateProposalCommand(string Name, decimal Value) : IRequest<Guid>;
