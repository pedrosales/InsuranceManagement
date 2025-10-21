
using Application.Proposal.Get;
using Domain.Proposal;
using MediatR;

namespace Web.Api.Endpoints.Proposals;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("proposals", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetProposalsQuery();

            IEnumerable<ProposalResponse> proposals = await sender.Send(command, cancellationToken);

            return Results.Ok(proposals);
        });
    }
}
