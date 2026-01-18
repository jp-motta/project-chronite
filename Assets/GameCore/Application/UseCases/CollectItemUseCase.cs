// Application/UseCases/CollectItemUseCase.cs
using Application.Common;
using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases
{
  public class CollectItemUseCase
  {
    private readonly IInventoryRepository _inventoryRepo;

    public CollectItemUseCase(IInventoryRepository inventoryRepo)
    {
      _inventoryRepo = inventoryRepo;
    }

    public Result<Item> Execute(Item item)
    {
      if (item == null)
        return Result<Item>.Failure("Item cannot be null");

      var inventory = _inventoryRepo.Load();

      inventory.AddItem(item);
      _inventoryRepo.Save(inventory);

      return Result<Item>.Success(item);
    }
  }
}
