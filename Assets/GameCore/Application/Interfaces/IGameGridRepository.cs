using Domain.Entities;

namespace Application
{
  public interface IGameGridRepository
  {
    GameGrid Load();
    void Save(GameGrid grid);
  }
}
