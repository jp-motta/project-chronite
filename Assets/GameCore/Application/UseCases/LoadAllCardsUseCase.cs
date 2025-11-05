using System.Collections.Generic;
using Application.Interfaces;

namespace Application.UseCases
{
  public class LoadAllCardsUseCase
  {
    private readonly ICardRepository cardRepo;

    public LoadAllCardsUseCase(ICardRepository cardRepo)
    {
      this.cardRepo = cardRepo;
    }

    public List<Domain.Entities.Card> Execute()
    {
      return cardRepo.LoadAll();
    }
  }
}
