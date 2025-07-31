public class Sprint
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Goal { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public ICollection<Issue> Issues { get; set; }
}