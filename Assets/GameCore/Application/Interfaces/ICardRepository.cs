using System.Collections.Generic;
using Domain.Entities;

namespace Application.Interfaces
{
  public interface ICardRepository
  {
    List<Card> LoadAll();
    Card? GetById(string id);
  }
}
