using Application.Abstractions.Data;
using Domain.Proposal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Proposal.ChangeStatus;

internal sealed class ChangeStatusCommandHandler(IApplicationDbContext context) : IRequestHandler<ChangeStatusCommand, bool>
{
    public async Task<bool> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
    {
        ProposalItem? proposalItem = await context.Proposals.SingleOrDefaultAsync(p => p.Id == command.ProposalId, cancellationToken) 
            ?? throw new InvalidOperationException($"Proposal with ID '{command.ProposalId}' does not exist.");

        switch (command.NewStatus)
        {
            case Status.Approved:
                proposalItem.Approve();
                break;
            case Status.Rejected:
                proposalItem.Reject();
                break;
            case Status.UnderReview:
            default:
                break;
        }

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
