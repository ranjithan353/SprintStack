using SprintStack.Enum;

public class Label
{
    public long Id { get; set; }
    public Labels Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Issue> Issues { get; set; }
}