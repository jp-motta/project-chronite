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

      var validFeatures = new List<Feature>();

      if (so.features != null)
      {
        foreach (var featureSO in so.features)
        {
          if (featureSO != null)
          {
            validFeatures.Add(new Feature
            {
              Type = (Domain.Entities.FeatureType)featureSO.type,
              Value = featureSO.value
            });
          }
        }
      }

      var cell = new Cell(so.x, so.y, validFeatures.ToArray());

      if (so.exploredInitially)
        cell.MarkAsExplored();

      dict[(so.x, so.y)] = cell;
    }

    Debug.Log($"[SOGameGridRepository] Loaded {dict.Count} cells from GridData.");
    return new GameGrid(dict);
  }
}
