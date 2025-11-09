using Application.Interfaces;
using Domain.Entities;
using System.Linq;
using UnityEngine;

public class DeckRepository : IDeckRepository
{
  private Deck _deck;
  private readonly CardDatabaseSO database;

  public DeckRepository(CardDatabaseSO cardDatabase)
  {
    database = cardDatabase;
  }

  public Deck Load()
  {
    if (_deck == null)
      _deck = CreateDefaultDeck();

    return _deck;
  }

  public void Save(Deck deck) => _deck = deck;

  private Deck CreateDefaultDeck()
  {
    var deck = new Deck();

    var source = database?.Cards;
    if (source == null || source.Count == 0)
    {
      Debug.LogWarning("[DeckRepository] Database is null or has no cards.");
      return deck;
    }

    var cards = source
      .Where(c => c != null)
      .Select(c => c.ToDomain())
      .ToList();

    Debug.Log($"[DeckRepository] Loaded {cards.Count}/{source.Count} cards from database.");
    deck.AddToDrawRange(cards);

    return deck;
  }
}
