using Application.Abstractions.Data;
using Domain.Proposal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Proposal.Create;

internal sealed class CreateProposalCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateProposalCommand, Guid>
{
    public async Task<Guid> Handle(CreateProposalCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command?.Name))
        {
            throw new ArgumentNullException(nameof(command), "Proposal name cannot be null or empty.");
        }

        if(await context.Proposals.AnyAsync(p => p.Name == command.Name, cancellationToken))
        {
            throw new InvalidOperationException($"A proposal with the name '{command.Name}' already exists.");
        }

        var proposalItem = new ProposalItem
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Value = command.Value,
            Status = Status.UnderReview,
            CreatedAt = DateTime.UtcNow
        };

        context.Proposals.Add(proposalItem);

        await context.SaveChangesAsync(cancellationToken);

        return proposalItem.Id;
    }
}
