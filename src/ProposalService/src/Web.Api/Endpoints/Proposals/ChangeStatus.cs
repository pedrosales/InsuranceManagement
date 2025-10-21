using Application.Proposal.ChangeStatus;
using Domain.Proposal;
using MediatR;

namespace Web.Api.Endpoints.Proposals;

internal sealed class ChangeStatus : IEndpoint
{
    public sealed record RequestChangeStatus(Guid Id, Status NewStatus);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("proposals/changestatus", async (RequestChangeStatus request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ChangeStatusCommand(request.Id, request.NewStatus);

            bool result = await sender.Send(command, cancellationToken);

            return result ? Results.NoContent() : Results.NotFound();
        });
    }
}
