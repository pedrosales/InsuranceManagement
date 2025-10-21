using Domain.Proposal;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<ProposalItem> Proposals { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
