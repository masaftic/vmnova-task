namespace vmnova.Domain.Users;

public class User
{
    public int Id { get; init; }
    public string Name { get; private set; } = null!;

    private readonly List<Role> _roles = [];
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

    private User() { }

    public static User Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new User
        {
            Name = name
        };
    }

    public void AddRole(Role role)
    {
        if (_roles.Contains(role))
            return;

        _roles.Add(role);
    }
}
