public class Team
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public long CreatedById { get; set; }

    public ICollection<TeamMember> Members { get; set; }
    
    
}