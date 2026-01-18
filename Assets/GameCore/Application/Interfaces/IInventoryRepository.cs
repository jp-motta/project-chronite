// Application/Interfaces/IInventoryRepository.cs
using Domain.Entities;

namespace Application.Interfaces
{
  public interface IInventoryRepository
  {
    Inventory Load();
    void Save(Inventory inventory);
  }
}
