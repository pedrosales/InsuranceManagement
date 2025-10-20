using Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Proposal.GetById;

internal sealed class GetProposalByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProposalByIdQuery, ProposalResponse>
{
    public async Task<ProposalResponse> Handle(GetProposalByIdQuery query, CancellationToken cancellationToken)
    {
        ProposalResponse proposal = await context.Proposals
            .Where(p => p.Id == query.ProposalId)
            .Select(p => new ProposalResponse
            {
                Id = p.Id,
                Name = p.Name,
                Value = p.Value,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            })
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InvalidOperationException($"Proposal with ID '{query.ProposalId}' does not exist.");

        return proposal;
    }
}
