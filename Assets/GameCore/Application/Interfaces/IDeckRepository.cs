using Domain.Entities;

namespace Application.Interfaces
{
  public interface IDeckRepository
  {
    Deck Load();
    void Save(Deck deck);
  }
}
