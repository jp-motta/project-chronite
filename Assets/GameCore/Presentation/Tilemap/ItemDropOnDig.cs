// Presentation/Tilemap/ItemDropOnDig.cs
using UnityEngine;
using UnityEngine.Tilemaps;
using Domain.Entities;
using Infrastructure.ScriptableObjects;

public class ItemDropOnDig : MonoBehaviour
{
  [SerializeField] private Tilemap tilemap;
  [SerializeField] private CellItemDropDatabaseSO dropDatabase;
  [SerializeField] private GameObject itemPickupPrefab;

  public void OnCellDug(Vector3Int cellPos, Cell dugCell)
  {
    if (dugCell == null || dropDatabase == null || itemPickupPrefab == null)
      return;

    // Consulta o banco de dados para ver se esse feature type tem drop
    var itemDrop = dropDatabase.GetDropForFeature(dugCell.Feature);
    if (itemDrop == null)
      return;

    // Instancia o item na posição da célula
    Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);
    var itemPickup = Instantiate(itemPickupPrefab, worldPos, Quaternion.identity);

    // Atribui o item ao pickup
    var pickupComponent = itemPickup.GetComponent<ItemPickup>();
    if (pickupComponent != null)
    {
      pickupComponent.SetItemData(itemDrop);
    }
  }
}
