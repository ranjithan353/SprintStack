public class Project
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool IsActive { get; set; }

    public long CreatedById { get; set; }
    public User CreatedBy { get; set; }

    public ICollection<Issue> Issues { get; set; }
}