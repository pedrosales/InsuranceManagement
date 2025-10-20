namespace Domain.Proposal;

public sealed class ProposalItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Approve()
    {
        if (Status == Status.Approved)
        {
            return;
        }

        if (Status == Status.Rejected)
        {
            throw new InvalidOperationException("Cannot approve a rejected proposal.");
        }

        Status = Status.Approved;
    }

    public void Reject()
    {
        if (Status == Status.Rejected)
        {
            return;
        }
        if (Status == Status.Approved)
        {
            throw new InvalidOperationException("Cannot reject an approved proposal.");
        }
        Status = Status.Rejected;
    }
}
