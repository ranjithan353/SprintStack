public class IssueFile
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public string FilePath { get; set; }
    public long UploadUserId { get; set; }

    public Issue Issue { get; set; }
    public User UploadUser { get; set; }
}