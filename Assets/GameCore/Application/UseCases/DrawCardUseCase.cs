using Application.Common;
using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases
{
  public class DrawCardUseCase
  {
    private readonly IDeckRepository _deckRepo;
    private readonly IHandRepository _handRepo;

    public DrawCardUseCase(IDeckRepository deckRepo, IHandRepository handRepo)
    {
      _deckRepo = deckRepo;
      _handRepo = handRepo;
    }

    public Result<Card> Execute()
    {
      var deck = _deckRepo.Load();
      var hand = _handRepo.Load();

      if (hand.IsFull)
        return Result<Card>.Failure("Hand is full");

      var card = deck.Draw();
      if (card == null)
        return Result<Card>.Failure("No cards left in deck");

      hand.Add(card);

      _deckRepo.Save(deck);
      _handRepo.Save(hand);

      return Result<Card>.Success(card);
    }
  }
}
