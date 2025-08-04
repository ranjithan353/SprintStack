using SprintStack.Enum;

public class Status
{
    public long Id { get; set; }
    public SprintStack.Enum.Status Name { get; set; }

    public ICollection<Issue> Issues { get; set; }
}