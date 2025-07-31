public class Issue
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? DevEstimationDay { get; set; }
    public bool IsActive { get; set; }

    public long CreatedById { get; set; }
    public long ProjectId { get; set; }
    public long? AssigneeId { get; set; }
    public long? ReporteeId { get; set; }
    public long? PriorityId { get; set; }
    public long? SprintId { get; set; }
    public long? StatusId { get; set; }
    public long? LabelId { get; set; }
    public long? QaId { get; set; }

    public User CreatedBy { get; set; }
    public Project Project { get; set; }
    public User Assignee { get; set; }
    public User Reportee { get; set; }
    public Priority Priority { get; set; }
    public Sprint Sprint { get; set; }
    public Status StatusEntity { get; set; }
    public Label Label { get; set; }
    public User Qa { get; set; }

    public ICollection<Comment> Comments { get; set; }
    public ICollection<IssueFile> Files { get; set; }

    // ✅ Add this
}
