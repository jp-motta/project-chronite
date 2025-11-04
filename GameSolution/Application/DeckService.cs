// ...existing code...
using Domain.Entities;

namespace Application
{
  public class DeckService
  {
    private readonly Deck deck;
    private readonly Hand hand;

    public DeckService(Deck deck, Hand hand)
    {
      this.deck = deck;
      this.hand = hand;
    }

    public Card? DrawCard()
    {
      if (!hand.CanAdd)
        return null;

      var card = deck.Draw();
      if (card != null)
        hand.Add(card);

      return card;
    }

    public void UseCard(Card card)
    {
      hand.Remove(card);
      deck.AddToDiscard(card);
    }
  }
}
// ...existing code...
