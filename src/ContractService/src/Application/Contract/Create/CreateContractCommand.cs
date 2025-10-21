using MediatR;

namespace Application.Contract.Create;

public sealed record CreateContractCommand(Guid ProposalId) : IRequest;
