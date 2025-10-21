using Application.Abstractions.Data;
using Domain.Proposal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Proposal.Delete;

internal sealed class DeleteProposalCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteProposalCommand, bool>
{
    public async Task<bool> Handle(DeleteProposalCommand command, CancellationToken cancellationToken)
    {
        ProposalItem? proposalItem = await context.Proposals.SingleOrDefaultAsync(p => p.Id == command.ProposalId, cancellationToken)
            ?? throw new InvalidOperationException($"Proposal with ID '{command.ProposalId}' does not exist.");

        context.Proposals.Remove(proposalItem);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
