using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Application;
using Domain.Entities;

public class UnityGameGridRepository : IGameGridRepository
{
  private readonly Tilemap _tilemap;

  public UnityGameGridRepository(Tilemap tilemap)
  {
    _tilemap = tilemap;
  }

  public GameGrid Load()
  {
    var cells = new Dictionary<(int, int), Cell>();

    foreach (var pos in _tilemap.cellBounds.allPositionsWithin)
    {
      var tile = _tilemap.GetTile(pos);

      if (tile != null)
        cells[(pos.x, pos.y)] = new Cell(pos.x, pos.y);
    }

    return new GameGrid(cells);
  }

  public void Save(GameGrid grid)
  {
    // no futuro → persistência real
  }
}
