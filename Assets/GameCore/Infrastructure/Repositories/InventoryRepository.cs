// Infrastructure/Repositories/InventoryRepository.cs
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
  public class InventoryRepository : IInventoryRepository
  {
    private Inventory _inventory = new();

    public Inventory Load()
    {
      return _inventory;
    }

    public void Save(Inventory inventory)
    {
      _inventory = inventory ?? new Inventory();
    }
  }
}
