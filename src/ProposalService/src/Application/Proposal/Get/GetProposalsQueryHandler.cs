using Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Proposal.Get;

internal sealed class GetProposalsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProposalsQuery, List<ProposalResponse>>
{
    public async Task<List<ProposalResponse>> Handle(GetProposalsQuery query, CancellationToken cancellationToken)
    {
        List<ProposalResponse> proposals = await context.Proposals
            .Select(p => new ProposalResponse
            {
                Id = p.Id,
                Name = p.Name,
                Value = p.Value,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return proposals;
    }
}
