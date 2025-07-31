public class TeamMember
{
    public long Id { get; set; }
    public long TeamId { get; set; }
    public long UserId { get; set; }

    public Team Team { get; set; }
    public User User { get; set; }
}