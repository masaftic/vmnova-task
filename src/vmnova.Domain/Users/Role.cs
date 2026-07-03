namespace vmnova.Domain.Users;

public class Role
{
    public int Id { get; init; }
    public string Name { get; private set; } = null!;

    private readonly List<Permission> _permissions = [];
    public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();

    private Role() { }

    public static Role Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Role
        {
            Name = name
        };
    }

    public void AddPermission(Permission permission)
    {
        if (_permissions.Contains(permission))
            return;

        _permissions.Add(permission);
    }
}
