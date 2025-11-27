using UnityEngine.Tilemaps;
using Domain.Entities;

public static class TilemapRendererUtility
{
  public static void RemoveCell(Tilemap tilemap, Cell cell)
  {
    var pos = new UnityEngine.Vector3Int(cell.X, cell.Y, 0);
    tilemap.SetTile(pos, null);
  }
}
