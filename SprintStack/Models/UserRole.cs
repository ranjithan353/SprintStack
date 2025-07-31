using SprintStack.Enum;

public class UserRole
{
    public long Id { get; set; }
    public UserRoleEnum Name { get; set; }

    public ICollection<User> Users { get; set; }
}