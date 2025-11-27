namespace Domain.Entities
{
  public interface IDigService
  {
    DigResult Dig(GameGrid grid, int positionX, int positionY);
  }
}
