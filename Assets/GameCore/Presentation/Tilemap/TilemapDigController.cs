// Presentation/Tilemap/TilemapDigController.cs
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Domain.Entities;
using Infrastructure.ScriptableObjects;

public class TilemapDigController : MonoBehaviour
{
  [SerializeField] private Tilemap tilemap;
  [SerializeField] private Transform player;
  [SerializeField] private float rayDistance = 1.5f;
  [SerializeField] private LayerMask digLayer;
  [SerializeField] private TileItemDropDatabaseSO dropDatabase;

  [Header("Highlight")]
  [SerializeField] private Color highlightColor = new Color(1f, 1f, 1f, 0.65f);

  private PlayerDigOrientation orientation;
  private ItemDropOnDig itemDropOnDig;

  private Vector3Int lastHighlightedCell;
  private bool hasHighlight;

  private void Awake()
  {
    orientation = player.GetComponent<PlayerDigOrientation>();
    itemDropOnDig = FindObjectOfType<ItemDropOnDig>();

    if (itemDropOnDig == null)
    {
      Debug.LogWarning("[TilemapDigController] ItemDropOnDig not found in scene");
    }
  }

  private void Update()
  {
    UpdateHighlight();

    if (Keyboard.current == null)
      return;

    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
      TryDig();
    }
  }

  private void UpdateHighlight()
  {
    if (tilemap == null || player == null) return;

    Vector2 dir = GetFacingVector();
    Vector2 origin = (Vector2)player.position + dir * 0.1f;

    RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDistance, digLayer);
    Vector3Int cellPos = GetTargetCell(hit, dir, origin);

    if (hasHighlight && lastHighlightedCell != cellPos)
    {
      if (tilemap.HasTile(lastHighlightedCell))
      {
        tilemap.SetTileFlags(lastHighlightedCell, TileFlags.None);
        tilemap.SetColor(lastHighlightedCell, Color.white);
      }
      hasHighlight = false;
    }

    if (tilemap.HasTile(cellPos))
    {
      tilemap.SetTileFlags(cellPos, TileFlags.None);
      tilemap.SetColor(cellPos, highlightColor);
      lastHighlightedCell = cellPos;
      hasHighlight = true;
    }
  }

  private void TryDig()
  {
    Vector2 dir = GetFacingVector();
    Vector2 origin = (Vector2)player.position + dir * 0.1f;

    RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDistance, digLayer);
    Vector3Int cellPos = GetTargetCell(hit, dir, origin);

    TileBase tile = tilemap.GetTile(cellPos);
    if (tile == null)
    {
      Debug.LogWarning($"[Dig] No tile at {cellPos}");
      return;
    }

    Debug.Log($"[Dig] Digging tile {tile.name} at {cellPos}");

    if (itemDropOnDig != null)
    {
      itemDropOnDig.OnTileAboutToDig(cellPos, tile);
    }

    tilemap.SetTile(cellPos, null);

    if (hasHighlight && lastHighlightedCell == cellPos)
    {
      hasHighlight = false;
    }
  }

  private Vector3Int GetTargetCell(RaycastHit2D hit, Vector2 dir, Vector2 origin)
  {
    if (hit.collider != null)
    {
      Vector2 biased = hit.point + dir * 0.01f;
      return tilemap.WorldToCell(biased);
    }

    Vector3Int playerCell = tilemap.WorldToCell(player.position);
    Vector3Int offset = DirToCellOffset(dir);
    return playerCell + offset;
  }

  private Vector2 GetFacingVector()
  {
    var d = orientation.GetDirection();
    return d switch
    {
      Direction.Left => Vector2.left,
      Direction.Right => Vector2.right,
      Direction.Up => Vector2.up,
      Direction.Down => Vector2.down,
      _ => Vector2.right
    };
  }

  private static Vector3Int DirToCellOffset(Vector2 dir)
  {
    int dx = dir.x > 0.5f ? 1 : dir.x < -0.5f ? -1 : 0;
    int dy = dir.y > 0.5f ? 1 : dir.y < -0.5f ? -1 : 0;
    return new Vector3Int(dx, dy, 0);
  }

  private void OnDisable()
  {
    if (hasHighlight && tilemap != null)
    {
      tilemap.SetTileFlags(lastHighlightedCell, TileFlags.None);
      tilemap.SetColor(lastHighlightedCell, Color.white);
      hasHighlight = false;
    }
  }
}
