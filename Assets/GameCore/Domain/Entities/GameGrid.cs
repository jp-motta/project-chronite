using System.Collections.Generic;

namespace Domain.Entities
{
  public class GameGrid
  {
    private readonly Dictionary<(int, int), Cell> _cells;

    public GameGrid(Dictionary<(int, int), Cell> cells)
    {
      _cells = cells;
    }

    public Cell GetCell(int x, int y)
    {
      _cells.TryGetValue((x, y), out var c);
      return c;
    }

    public bool Contains(int x, int y) => _cells.ContainsKey((x, y));
  }
}
