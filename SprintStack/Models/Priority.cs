using SprintStack.Enum;

public class Priority
{
    public long Id { get; set; }
    public Priorities Name { get; set; }

    public ICollection<Issue> Issues { get; set; }
}
