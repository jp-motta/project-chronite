// Presentation/Tilemap/ItemDropOnDig.cs
using UnityEngine;
using UnityEngine.Tilemaps;
using Infrastructure.ScriptableObjects;

public class ItemDropOnDig : MonoBehaviour
{
  [SerializeField] private Tilemap tilemap;
  [SerializeField] private TileItemDropDatabaseSO dropDatabase;

  private void Awake()
  {
    if (tilemap == null)
    {
      tilemap = FindObjectOfType<Tilemap>();
    }
  }

  public void OnTileAboutToDig(Vector3Int cellPos, TileBase tile)
  {
    if (tile == null || dropDatabase == null)
    {
      return;
    }

    var drop = dropDatabase.GetDropForTile(tile);
    if (drop == null || drop.itemPickupPrefab == null || drop.itemDrop == null)
    {
      return;
    }

    Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);
    var itemPickup = Instantiate(drop.itemPickupPrefab, worldPos, Quaternion.identity);

    var pickupComponent = itemPickup.GetComponent<ItemPickup>();
    if (pickupComponent != null)
    {
      pickupComponent.SetItemData(drop.itemDrop);
      Debug.Log($"[ItemDropOnDig] Dropped {drop.itemDrop.ItemName} at {cellPos}");
    }
  }
}
