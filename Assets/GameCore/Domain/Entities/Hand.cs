using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Entities
{
  public class Hand
  {
    private readonly List<Card> _cards = new();
    private readonly int _maxSize;

    public IReadOnlyList<Card> Cards => _cards;

    public Hand(int maxSize)
    {
      _maxSize = maxSize;
    }

    public bool CanAdd => _cards.Count < _maxSize;

    public void Add(Card card)
    {
      if (!CanAdd)
        throw new Exception("Hand is full!");

      _cards.Add(card);
    }

    public void Remove(Card card)
    {
      _cards.Remove(card);
    }

    public bool Contains(Card card)
    {
      return _cards.Contains(card);
    }
  }
}
