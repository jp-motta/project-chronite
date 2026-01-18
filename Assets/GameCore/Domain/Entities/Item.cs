// Domain/Entities/Item.cs
namespace Domain.Entities
{
  public class Item
  {
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }

    public Item(string id, string name, string description)
    {
      if (string.IsNullOrWhiteSpace(id))
        throw new System.ArgumentException("Item ID cannot be empty", nameof(id));
      if (string.IsNullOrWhiteSpace(name))
        throw new System.ArgumentException("Item name cannot be empty", nameof(name));

      Id = id;
      Name = name;
      Description = description ?? string.Empty;
    }
  }
}
