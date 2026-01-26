// Presentation/Items/ItemPickup.cs
using UnityEngine;
using Application.UseCases;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.ScriptableObjects;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
  [SerializeField] private ItemDataSO itemData;
  [SerializeField] private string playerTag = "Player";

  private CollectItemUseCase collectUseCase;
  private InventoryPresenter inventoryPresenter;
  private bool collected;

  private void Awake()
  {
    var collider = GetComponent<Collider2D>();

    collectUseCase = new CollectItemUseCase(InventoryRepository.Instance);

    inventoryPresenter = FindObjectOfType<InventoryPresenter>();
    if (inventoryPresenter == null)
    {
      Debug.LogWarning("[ItemPickup] InventoryPresenter not found in scene");
    }
  }

  public void SetItemData(ItemDataSO data)
  {
    itemData = data;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collected) return;
    if (collision.gameObject.CompareTag(playerTag) == false) return;

    if (itemData == null)
    {
      Debug.LogError("[ItemPickup] ItemDataSO not assigned", gameObject);
      return;
    }

    Item item = itemData.ToDomain();
    var result = collectUseCase.Execute(item);

    if (result.IsSuccess)
    {
      Debug.Log($"[ItemPickup] Collected: {item.Name}");

      if (inventoryPresenter != null)
      {
        inventoryPresenter.RefreshUI();
      }

      collected = true;
      Destroy(gameObject);
    }
    else
    {
      Debug.LogWarning($"[ItemPickup] Failed to collect: {result.Error}");
    }
  }
}
