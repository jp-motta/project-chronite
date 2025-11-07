using Application.Interfaces;
using Domain.Entities;

public class DeckRepository : IDeckRepository
{
  private readonly CardDatabaseSO database;

  public DeckRepository(CardDatabaseSO cardDatabase)
  {
    database = cardDatabase;
  }

  public Deck Load()
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

  public void Save(Deck deck)
  {
    // Nada a salvar por enquanto — deck é recriado a cada partida.
  }
}
