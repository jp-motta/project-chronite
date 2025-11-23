using System.Collections.Generic;
using UnityEngine;
using Domain.Entities;

[CreateAssetMenu(menuName = "Game/Grid/Game Grid")]
public class GameGridSO : ScriptableObject
{
  [System.Serializable]
  public class CellData
  {
    public int x;
    public int y;
    public bool exploredInitially;
    public FeatureType feature;
  }

  [SerializeField] private List<CellData> cells = new();
  public IReadOnlyList<CellData> Cells => cells;

  public void AddCell(int x, int y)
  {
    cells.Add(new CellData { x = x, y = y, exploredInitially = false, feature = FeatureType.Empty });
  }

  public void RemoveCellAt(int x, int y)
  {
    int idx = cells.FindIndex(c => c.x == x && c.y == y);
    if (idx >= 0) cells.RemoveAt(idx);
  }

  public CellData Find(int x, int y) => cells.Find(c => c.x == x && c.y == y);
}
