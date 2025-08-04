using SprintStack.Enum;

public class UserRole
{
    public long Id { get; set; }
    public UserRoles Name { get; set; }

    public ICollection<User> Users { get; set; }
}