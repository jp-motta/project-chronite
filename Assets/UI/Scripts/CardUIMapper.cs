using System.Collections.Generic;
using UnityEngine;
using Domain.Entities;

[DisallowMultipleComponent]
public class CardUIMapper : MonoBehaviour
{
  [Header("Data Source")]
  [SerializeField] private CardDatabaseSO database;

  [Header("Fallbacks")]
  [SerializeField] private Sprite defaultArtwork;

  private readonly Dictionary<string, CardDataSO> _map = new();

  private void Awake() => BuildMap();

  private void BuildMap()
  {
    _map.Clear();

    if (database?.Cards == null || database.Cards.Count == 0)
    {
      Debug.LogWarning("[CardUIMapper] Database or Cards list is null/empty.");
      return;
    }

    foreach (var entry in database.Cards)
    {
      if (entry?.Id is null || entry.Id.Trim().Length == 0)
      {
        Debug.LogWarning("[CardUIMapper] Skipped entry with null/empty Id.");
        continue;
      }

      if (_map.ContainsKey(entry.Id))
      {
        Debug.LogWarning($"[CardUIMapper] Duplicate Id '{entry.Id}' ignored.");
        continue;
      }

      _map[entry.Id] = entry;
    }
  }

  public Sprite GetArtwork(Card card)
  {
    if (card == null || string.IsNullOrWhiteSpace(card.Id))
      return defaultArtwork;

    if (_map.TryGetValue(card.Id, out var data) && data is { Artwork: { } artwork })
      return artwork;

    return defaultArtwork;
  }
}
