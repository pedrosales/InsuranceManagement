using Application.Proposal.GetById;
using MediatR;

namespace Api.Endpoints.Proposals;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("proposals/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetProposalByIdQuery(id);

            ProposalResponse? proposal = await sender.Send(command, cancellationToken);

            if (proposal is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(proposal);
        });
    }
}
