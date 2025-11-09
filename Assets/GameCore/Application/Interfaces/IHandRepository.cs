using Domain.Entities;

namespace Application.Interfaces
{
  public interface IHandRepository
  {
    Hand Load();
    void Save(Hand hand);
  }
}
