using System.Collections.Generic;
using Domain.Exceptions;

namespace Domain.Entities
{
  public class Hand
  {
    private readonly List<Card> _cards;
    private readonly int _maxSize;

    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();
    public bool IsFull => _cards.Count >= _maxSize;
    public int Count => _cards.Count;

    public Hand(int maxSize)
    {
      if (maxSize <= 0)
        throw new DomainException("Hand size must be greater than zero");

      _maxSize = maxSize;
      _cards = new List<Card>(_maxSize);
    }

    public void Add(Card card)
    {
      ValidateCard(card);

      if (IsFull)
        throw new DomainException($"Hand is full (max: {_maxSize} cards)");

      _cards.Add(card);
    }

    public void Remove(Card card)
    {
      ValidateCard(card);

      if (!_cards.Remove(card))
        throw new DomainException("Card not found in hand");
    }

    public bool Contains(Card card)
    {
      ValidateCard(card);
      return _cards.Contains(card);
    }

    private static void ValidateCard(Card card)
    {
      if (card == null)
        throw new DomainException("Card cannot be null");
    }
  }
}
