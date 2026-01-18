// Infrastructure/Repositories/InventoryRepository.cs
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
  public class InventoryRepository : IInventoryRepository
  {
    private static InventoryRepository instance;
    private Inventory _inventory;

    public static InventoryRepository Instance
    {
      get
      {
        instance ??= new InventoryRepository();
        return instance;
      }
    }

    private InventoryRepository()
    {
      _inventory = new Inventory();
    }

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
