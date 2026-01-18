// Domain/Entities/Inventory.cs
using System.Collections.Generic;
using Domain.Exceptions;

namespace Domain.Entities
{
  public class Inventory
  {
    private readonly Dictionary<string, (Item item, int quantity)> _items;
    public IEnumerable<(Item item, int quantity)> GetItems() => _items.Values;

    public Inventory()
    {
      _items = new Dictionary<string, (Item, int)>();
    }

    public void AddItem(Item item)
    {
      if (item == null)
        throw new DomainException("Item cannot be null");

      if (_items.ContainsKey(item.Id))
      {
        var (existingItem, quantity) = _items[item.Id];
        _items[item.Id] = (existingItem, quantity + 1);
      }
      else
      {
        _items[item.Id] = (item, 1);
      }
    }

    public void RemoveItem(Item item)
    {
      if (item == null)
        throw new DomainException("Item cannot be null");

      if (!_items.ContainsKey(item.Id))
        throw new DomainException("Item not found in inventory");

      var (existingItem, quantity) = _items[item.Id];
      if (quantity > 1)
      {
        _items[item.Id] = (existingItem, quantity - 1);
      }
      else
      {
        _items.Remove(item.Id);
      }
    }

    public bool Contains(Item item)
    {
      return item != null && _items.ContainsKey(item.Id);
    }

    public int GetQuantity(Item item)
    {
      if (item == null) return 0;
      return _items.ContainsKey(item.Id) ? _items[item.Id].quantity : 0;
    }

    public void Clear()
    {
      _items.Clear();
    }
  }
}
