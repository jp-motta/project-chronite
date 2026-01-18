// Domain/Entities/Inventory.cs
using System.Collections.Generic;
using Domain.Exceptions;

namespace Domain.Entities
{
  public class Inventory
  {
    private readonly List<Item> _items;

    public IReadOnlyList<Item> Items => _items.AsReadOnly();

    public Inventory()
    {
      _items = new List<Item>();
    }

    public void AddItem(Item item)
    {
      if (item == null)
        throw new DomainException("Item cannot be null");

      _items.Add(item);
    }

    public void RemoveItem(Item item)
    {
      if (item == null)
        throw new DomainException("Item cannot be null");

      if (!_items.Remove(item))
        throw new DomainException("Item not found in inventory");
    }

    public bool Contains(Item item)
    {
      return item != null && _items.Contains(item);
    }

    public void Clear()
    {
      _items.Clear();
    }
  }
}
