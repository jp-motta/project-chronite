using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using UnityEngine;

public class SOGameGridRepository : IGameGridRepository
{
  private readonly GameGridSO source;

  public SOGameGridRepository(GameGridSO gridData)
  {
    source = gridData;
  }

  public GameGrid Load()
  {
    if (source == null || source.cells == null || source.cells.Length == 0)
    {
      Debug.LogWarning("[SOGameGridRepository] Source is null or has no cells.");
      return new GameGrid(new Dictionary<(int, int), Cell>());
    }

    var dict = new Dictionary<(int, int), Cell>();

    foreach (var so in source.cells)
    {
      if (so == null)
      {
        Debug.LogWarning("[SOGameGridRepository] Null CellSO found in cells array.");
        continue;
      }

      var features = new Feature[so.features?.Length ?? 0];
      for (int i = 0; i < features.Length; i++)
      {
        features[i] = new Feature
        {
          Type = so.features[i].type,
          Value = so.features[i].value
        };
      }

      var cell = new Cell(so.x, so.y, features);

      if (so.exploredInitially)
        cell.MarkAsExplored();

      dict[(so.x, so.y)] = cell;
    }

    Debug.Log($"[SOGameGridRepository] Loaded {dict.Count} cells from GridData.");
    return new GameGrid(dict);
  }
}
