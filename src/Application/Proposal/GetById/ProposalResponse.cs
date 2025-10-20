using Domain.Proposal;

namespace Application.Proposal.GetById;

public sealed class ProposalResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
