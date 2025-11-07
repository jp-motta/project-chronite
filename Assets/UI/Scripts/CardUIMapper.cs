using System.Collections.Generic;
using UnityEngine;
using Domain.Entities;

public class CardUIMapper : MonoBehaviour
{
  [SerializeField] private List<CardDataSO> cards;

  private Dictionary<string, CardDataSO> map;

  void Awake()
  {
    map = new Dictionary<string, CardDataSO>();
    foreach (var c in cards)
      map[c.Id] = c;
  }

  public Sprite GetArtwork(Card card)
  {
    return map[card.Id].Artwork;
  }
}
