using UnityEngine;
using Domain.Entities;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CellFeatureMap", menuName = "Game/Features/Cell Feature Datatabase")]
public class CellFeatureDatabaseSO : ScriptableObject
{
  [System.Serializable]
  private struct Entry
  {
    public FeatureType type;
    public Sprite sprite;
    public Color color;
  }

  [SerializeField] private List<Entry> entries = new();
  [SerializeField] private Color fallbackColor = new Color(0.45f, 0.30f, 0.12f, 0.35f);

  public Sprite GetSprite(FeatureType type)
  {
    for (int i = 0; i < entries.Count; i++)
      if (entries[i].type == type)
        return entries[i].sprite;
    return null;
  }

  public Color GetColor(FeatureType type)
  {
    for (int i = 0; i < entries.Count; i++)
      if (entries[i].type == type)
        return entries[i].color.a > 0 ? entries[i].color : fallbackColor;
    return fallbackColor;
  }
}
