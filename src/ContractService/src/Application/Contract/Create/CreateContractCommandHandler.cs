using Application.Abstractions.Data;
using Domain.Contract;
using MediatR;

namespace Application.Contract.Create;

internal sealed class CreateContractCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateContractCommand>
{
    public async Task Handle(CreateContractCommand command, CancellationToken cancellationToken)
    {
        if (command.ProposalId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(command), "ProposalId cannot be empty.");
        }

        var contractItem = new ContractItem
        {
            Id = Guid.NewGuid(),
            ProposalId = command.ProposalId,
            StartDate = DateTime.UtcNow
        };

        try
        {
            context.Contracts.Add(contractItem);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }


        return;
    }
}
