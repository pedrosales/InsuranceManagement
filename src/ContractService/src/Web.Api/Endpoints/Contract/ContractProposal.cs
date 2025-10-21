
using Application.Contract.Proposal;
using MediatR;

namespace Web.Api.Endpoints.Contract;

internal sealed class ContractProposal : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("contract/{id:guid}/proposal", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ContractProposalCommand(id);

            await sender.Send(command, cancellationToken);

            return Results.Created();
        });
    }
}
