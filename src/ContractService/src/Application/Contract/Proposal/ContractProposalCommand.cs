using MediatR;

namespace Application.Contract.Proposal;

public sealed record ContractProposalCommand(Guid ProposalId) : IRequest;
