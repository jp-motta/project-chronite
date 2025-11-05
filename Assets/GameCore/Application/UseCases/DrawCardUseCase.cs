using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases
{
  public class DrawCardUseCase
  {
    private readonly IDeckRepository deckRepo;
    private readonly IHandRepository handRepo;

    public DrawCardUseCase(IDeckRepository deckRepo, IHandRepository handRepo)
    {
      this.deckRepo = deckRepo;
      this.handRepo = handRepo;
    }

    public Card? Execute()
    {
      var deck = deckRepo.Load();
      var hand = handRepo.Load();

      if (!hand.CanAdd)
        return null;

      var card = deck.Draw();

      if (card != null)
        hand.Add(card);

      deckRepo.Save(deck);
      handRepo.Save(hand);

      return card;
    }
  }
}
