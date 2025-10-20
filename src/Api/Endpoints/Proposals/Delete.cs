
using Application.Proposal.Delete;
using MediatR;

namespace Api.Endpoints.Proposals;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("proposals/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteProposalCommand(id);
            bool result = await sender.Send(command, cancellationToken);

            return result ? Results.NoContent() : Results.NotFound();
        });
    }
}
