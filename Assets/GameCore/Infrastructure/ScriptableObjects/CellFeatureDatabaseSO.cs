using UnityEngine;
using Domain.Entities;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CellFeatureMap", menuName = "Game/Features/Cell Feature Database")]
public class CellFeatureDatabaseSO : ScriptableObject
{
  [System.Serializable]
  private class Entry
  {
    public FeatureType type;
    public Sprite sprite;
  }

  [SerializeField] private List<Entry> entries = new();

  public Sprite GetSprite(FeatureType type)
  {
    for (int i = 0; i < entries.Count; i++)
      if (entries[i].type == type)
        return entries[i].sprite;
    return null;
  }
}
