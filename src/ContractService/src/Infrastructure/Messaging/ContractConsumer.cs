using Application.Contract.Create;
using MassTransit;
using MediatR;
using SharedKernel;

namespace Infrastructure.Messaging;

public sealed class ContractConsumer(ISender sender) : IConsumer<ContractProposalCreatedEvent>
{
    public async Task Consume(ConsumeContext<ContractProposalCreatedEvent> context)
    {
        ContractProposalCreatedEvent evento = context.Message;

        var command = new CreateContractCommand(evento.ProposalId);
        await sender.Send(command);

        return;
    }
}
