namespace vmnova.Domain.Users;

public class Permission
{
    public int Id { get; init; }
    public string Name { get; private set; } = null!;

    protected Permission() { }

    public Permission(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }
}

public class FilterPermission : Permission
{
    public string EntityName { get; set; } = null!;

    private FilterPermission() { }

    public FilterPermission(string entityName, string name) : base(name)
    {
        EntityName = entityName;
    }
}

public class ColumnPermission : Permission
{
    public string EntityName { get; set; } = null!;
    public string ColumnName { get; set; } = null!;

    private ColumnPermission() { }

    public ColumnPermission(string entityName, string columnName, string name) : base(name)
    {
        EntityName = entityName;
        ColumnName = columnName;
    }
}
