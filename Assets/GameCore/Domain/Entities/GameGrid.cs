using System.Collections.Generic;

namespace Domain.Entities
{
  public class GameGrid
  {
    private readonly Dictionary<(int, int), Cell> cells;

    public GameGrid(Dictionary<(int, int), Cell> cells)
    {
      this.cells = cells;
    }

    public Cell? GetCell(int x, int y)
    {
      return cells.TryGetValue((x, y), out var cell) ? cell : null;
    }

    public IEnumerable<Cell> GetAllCells() => cells.Values;
  }
}
