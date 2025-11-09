using Application.Common;
using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases
{
  public class UseCardUseCase
  {
    private readonly IDeckRepository _deckRepo;
    private readonly IHandRepository _handRepo;

    public UseCardUseCase(
        IDeckRepository deckRepo,
        IHandRepository handRepo)
    {
      _deckRepo = deckRepo;
      _handRepo = handRepo;
    }

    public Result<Card> Execute(Card card)
    {
      if (card == null)
        return Result<Card>.Failure("Card cannot be null");

      var deck = _deckRepo.Load();
      var hand = _handRepo.Load();

      if (!hand.Contains(card))
        return Result<Card>.Failure("Card is not in hand");

      hand.Remove(card);
      deck.AddToDiscard(card);

      _deckRepo.Save(deck);
      _handRepo.Save(hand);

      return Result<Card>.Success(card);
    }
  }
}
