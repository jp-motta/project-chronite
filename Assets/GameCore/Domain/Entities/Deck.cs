using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain.Entities
{
  public class Deck
  {
    private readonly List<Card> _drawPile;
    private readonly List<Card> _discardPile;
    private readonly Random _random;

    public int DrawPileCount => _drawPile.Count;
    public int DiscardPileCount => _discardPile.Count;
    public bool IsEmpty => DrawPileCount == 0 && DiscardPileCount == 0;

    public Deck(Random? random = null)
    {
      _drawPile = new List<Card>();
      _discardPile = new List<Card>();
      _random = random ?? new Random();
    }

    public void AddToDraw(Card card)
    {
      ValidateCard(card);
      _drawPile.Add(card);
    }

    public void AddToDiscard(Card card)
    {
      ValidateCard(card);
      _discardPile.Add(card);
    }

    public Card? Draw()
    {
      if (IsEmpty)
        return null;

      if (DrawPileCount == 0)
        Reshuffle();

      var card = _drawPile[0];
      _drawPile.RemoveAt(0);
      return card;
    }

    public void AddToDrawRange(IEnumerable<Card> cards)
    {
      if (cards is null) throw new DomainException("Cards cannot be null");
      foreach (var c in cards) AddToDraw(c);
    }

    private void Reshuffle()
    {
      if (DiscardPileCount == 0)
        return;

      var shuffled = _discardPile.OrderBy(_ => _random.Next()).ToList();
      _discardPile.Clear();
      _drawPile.AddRange(shuffled);
    }

    private static void ValidateCard(Card card)
    {
      if (card == null)
        throw new DomainException("Card cannot be null");
    }
  }
}
