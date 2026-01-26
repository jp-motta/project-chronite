// Infrastructure/ScriptableObjects/TileItemDropDatabaseSO.cs
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Infrastructure.ScriptableObjects
{
  [System.Serializable]
  public class TileItemDrop
  {
    public TileBase tile;
    public ItemDataSO itemDrop;
    public GameObject itemPickupPrefab;
  }

  [CreateAssetMenu(fileName = "TileItemDropDatabase", menuName = "Game/Tile Item Drop Database")]
  public class TileItemDropDatabaseSO : ScriptableObject
  {
    [SerializeField] private List<TileItemDrop> drops = new();

    public TileItemDrop GetDropForTile(TileBase tile)
    {
      if (tile == null) return null;
      return drops.Find(d => d.tile == tile);
    }
  }
}
