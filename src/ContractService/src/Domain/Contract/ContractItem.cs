namespace Domain.Contract;

public sealed class ContractItem
{
    public Guid Id { get; set; }
    public Guid ProposalId { get; set; }
    public DateTime StartDate { get; set; }
}
