// Presentation/UI/InventoryPresenter.cs
using UnityEngine;
using TMPro;
using Application.Interfaces;
using Infrastructure.Repositories;
using System.Linq;

public class InventoryPresenter : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI inventoryText;

  private IInventoryRepository inventoryRepository;

  private void Awake()
  {
    inventoryRepository = InventoryRepository.Instance;
  }

  private void Start()
  {
    RefreshUI();
  }

  public void RefreshUI()
  {
    if (inventoryText == null)
    {
      Debug.LogError("[InventoryPresenter] TextMeshProUGUI not assigned", gameObject);
      return;
    }

    var inventory = inventoryRepository.Load();
    var itemsList = inventory.GetItems();

    if (!itemsList.Any())
    {
      inventoryText.text = "Inventory: Empty";
      return;
    }

    var text = "Inventory:\n";
    foreach (var (item, quantity) in itemsList)
    {
      text += $"• {item.Name} x{quantity}\n";
    }

    inventoryText.text = text;
  }
}
