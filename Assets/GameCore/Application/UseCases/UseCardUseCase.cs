using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases
{
  public class UseCardUseCase
  {
    private readonly IDeckRepository deckRepo;
    private readonly IHandRepository handRepo;

    public UseCardUseCase(
        IDeckRepository deckRepo,
        IHandRepository handRepo)
    {
      this.deckRepo = deckRepo;
      this.handRepo = handRepo;
    }

    public void Execute(Card card)
    {
      var deck = deckRepo.Load();
      var hand = handRepo.Load();

      if (!hand.Contains(card))
        return;

      hand.Remove(card);
      deck.AddToDiscard(card);

      deckRepo.Save(deck);
      handRepo.Save(hand);
    }
  }
}
