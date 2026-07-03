namespace vmnova.Domain.Categories;

public class Category
{
    public string Id { get; init; } = null!;
    public string Name { get; private set; } = null!;
    public string IconSvg { get; private set; } = null!;

    private Category() { }

    public static Category Create(string id, string name, string iconSvg)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Category
        {
            Id = id,
            Name = name.Trim(),
            IconSvg = iconSvg
        };
    }

    public void Update(string name, string iconSvg)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name.Trim();
        IconSvg = iconSvg;
    }
}
