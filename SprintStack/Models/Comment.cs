public class Comment
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long AuthorId { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public Issue Issue { get; set; }
    public User Author { get; set; }
}