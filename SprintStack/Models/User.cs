public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool IsActive { get; set; }

    public long RoleId { get; set; }
    public UserRole Role { get; set; }

    public ICollection<TeamMember> TeamMembers { get; set; }
    public ICollection<Project> CreatedProjects { get; set; }
    public ICollection<Issue> CreatedIssues { get; set; }
    public ICollection<Issue> AssignedIssues { get; set; }
    public ICollection<Issue> ReportedIssues { get; set; }
    public ICollection<Issue> QaIssues { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<IssueFile> UploadedFiles { get; set; }
}