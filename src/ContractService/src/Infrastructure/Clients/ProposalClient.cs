using System.Net.Http.Json;
using Application.Abstractions;

namespace Infrastructure.Clients;

public sealed class ProposalClient(HttpClient httpClient) : IProposalClient
{
    private record ProposalResponse(Guid Id, string Name, int Status);
    public async Task<bool> IsProposalApprovedAsync(Guid proposalId, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"/proposals/{proposalId}", cancellationToken);

        ProposalResponse? proposal = await response.Content.ReadFromJsonAsync<ProposalResponse>(cancellationToken: cancellationToken);

        if (proposal is null)
        {
            return false;
        }

        return proposal.Status == 2;
        //return proposal.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase);
    }
}
