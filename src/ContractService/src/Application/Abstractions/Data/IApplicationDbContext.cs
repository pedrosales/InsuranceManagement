using Domain.Contract;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<ContractItem> Contracts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
