using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

public class SOGameGridRepository : IGameGridRepository
{
  private readonly GameGridSO source;

  public SOGameGridRepository(GameGridSO gridData) => source = gridData;

  public GameGrid Load()
  {
    var dict = new Dictionary<(int, int), Cell>();
    if (source == null || source.Cells == null) return new GameGrid(dict);

    foreach (var c in source.Cells)
    {
      var cell = new Cell(c.x, c.y, c.feature);
      if (c.exploredInitially) cell.MarkAsExplored();
      dict[(c.x, c.y)] = cell;
    }
    return new GameGrid(dict);
  }
}
