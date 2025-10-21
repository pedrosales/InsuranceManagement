using Domain.Proposal;
using MediatR;

namespace Application.Proposal.ChangeStatus;

public sealed record ChangeStatusCommand(Guid ProposalId, Status NewStatus) : IRequest<bool>;
