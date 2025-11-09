using System.Collections.Generic;
using UnityEngine;
using Domain.Entities;

public class CardUIMapper : MonoBehaviour
{
  [SerializeField] private CardDatabaseSO database;

  private Dictionary<string, CardDataSO> map;

  void Awake()
  {
    map = new Dictionary<string, CardDataSO>();

    foreach (var c in database.Cards)
      map[c.Id] = c;
  }

  public Sprite GetArtwork(Card card)
  {
    if (map.TryGetValue(card.Id, out var data))
      return data.Artwork;

    Debug.LogError($"CardUIMapper: não foi encontrado artwork para a carta ID={card.Id}");

    return null;
  }
}
