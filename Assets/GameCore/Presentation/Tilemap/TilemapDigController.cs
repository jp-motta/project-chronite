using UnityEngine;
using UnityEngine.Tilemaps;
using Application;
using Domain.Entities;
using UnityEngine.InputSystem;

public class TilemapDigController : MonoBehaviour
{
  [SerializeField] private Tilemap tilemap;
  [SerializeField] private Transform player;
  [SerializeField] private float rayDistance = 1.5f;
  [SerializeField] private LayerMask digLayer; // defina para colisor dos tiles

  [Header("Highlight")]
  [SerializeField] private Color highlightColor = new Color(1f, 1f, 1f, 0.65f);

  private DigUseCase digUseCase;
  private PlayerDigOrientation orientation;

  private Vector3Int lastHighlightedCell;
  private bool hasHighlight;

  private void Awake()
  {
    orientation = player.GetComponent<PlayerDigOrientation>();

    digUseCase = new DigUseCase(
        new DigService(),
        new UnityGameGridRepository(tilemap)
    );
  }

  private void Update()
  {
    // Atualiza highlight a cada frame
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

    Vector3Int cellPos = GetTargetCell();

    // If changed, clear previous
    if (hasHighlight && lastHighlightedCell != cellPos)
    {
      if (tilemap.HasTile(lastHighlightedCell))
      {
        tilemap.SetTileFlags(lastHighlightedCell, TileFlags.None);
        tilemap.SetColor(lastHighlightedCell, Color.white);
      }
      hasHighlight = false;
    }

    // Apply highlight if tile exists
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
    Vector3Int cellPos = GetTargetCell();
    Debug.Log($"[Dig] Trying cell {cellPos}");

    DigResult result = digUseCase.Execute(cellPos.x, cellPos.y);

    if (result.Success)
    {
      // Clear highlight if we removed the targeted cell
      if (hasHighlight && lastHighlightedCell == cellPos && tilemap.HasTile(lastHighlightedCell))
      {
        tilemap.SetTileFlags(lastHighlightedCell, TileFlags.None);
        tilemap.SetColor(lastHighlightedCell, Color.white);
        hasHighlight = false;
      }

      TilemapRendererUtility.RemoveCell(tilemap, result.DugCell);
      Debug.Log($"[Dig] Removed cell ({cellPos.x},{cellPos.y})");
    }
    else
    {
      Debug.LogWarning("[Dig] Dig failed");
    }
  }

  private void OnDisable()
  {
    // Garante limpeza do highlight ao desabilitar
    if (hasHighlight && tilemap != null)
    {
      tilemap.SetTileFlags(lastHighlightedCell, TileFlags.None);
      tilemap.SetColor(lastHighlightedCell, Color.white);
      hasHighlight = false;
    }
  }

  private Vector2 GetFacingVector()
  {
    Direction d = orientation.GetDirection();
    Debug.Log($"Direction: {d}");

    return d switch
    {
      Direction.Left => Vector2.left,
      Direction.Right => Vector2.right,
      Direction.Up => Vector2.up,
      Direction.Down => Vector2.down,
      _ => Vector2.right
    };
  }

  private Vector3Int GetTargetCell()
  {
    if (tilemap == null || player == null) return Vector3Int.zero;

    Vector2 dir = GetFacingVector();
    Vector2 origin = (Vector2)player.position + dir * 0.1f; // avoid self-hit
    RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayDistance, digLayer);

    if (hit.collider != null)
    {
      // Bias inside the tile along the ray direction to avoid edge-rounding issues
      Vector2 biased = hit.point + dir * 0.01f;
      Vector3Int cell = tilemap.WorldToCell(biased);
      // Debug
      Debug.Log($"[TargetCell] Hit at {hit.point} → biased {biased} → cell {cell} (dir {dir})");
      return cell;
    }

    // Fallback to the adjacent cell in facing direction
    Vector3Int playerCell = tilemap.WorldToCell(player.position);
    Vector3Int offset = DirToCellOffset(dir);
    Vector3Int target = playerCell + offset;
    Debug.Log($"[TargetCell] No hit. Fallback playerCell {playerCell} + {offset} → {target}");
    return target;
  }

  private static Vector3Int DirToCellOffset(Vector2 dir)
  {
    int dx = dir.x > 0.5f ? 1 : dir.x < -0.5f ? -1 : 0;
    int dy = dir.y > 0.5f ? 1 : dir.y < -0.5f ? -1 : 0;
    return new Vector3Int(dx, dy, 0);
  }
}
