using Application.Interfaces;
using Domain.Entities;
using System.Linq;

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

  public void Save(Deck deck)
  {
    _deck = deck;
  }

  private Deck CreateDefaultDeck()
  {
    var deck = new Deck();

    var cards = (database?.Cards?.ToArray() ?? System.Array.Empty<CardDataSO>())
      .Where(c => c != null && !string.IsNullOrWhiteSpace(c.Id))
      .Select(c => new Card(
          c.Id,
          c.Name,
          c.PickaxeCost,
          c.ArtworkId));

    deck.AddToDrawRange(cards);

    return deck;
  }
}
