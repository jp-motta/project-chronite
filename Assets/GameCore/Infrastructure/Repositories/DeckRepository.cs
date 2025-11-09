using Application.Interfaces;
using Domain.Entities;

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

    foreach (var cardSO in database.Cards)
    {
      var card = new Card(
          cardSO.Id,
          cardSO.Name,
          cardSO.PickaxeCost,
          cardSO.ArtworkId
      );

      deck.AddToDraw(card);
    }

    return deck;
  }
}
