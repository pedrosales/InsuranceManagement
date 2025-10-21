using MediatR;
using Application.Proposal.Create;

namespace Web.Api.Endpoints.Proposals;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("proposals", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateProposalCommand(request.Name, request.Value);

            Guid proposalId = await sender.Send(command, cancellationToken);

            return Results.Created($"/proposals/{proposalId}", new { Id = proposalId });
        });
    }
}
