namespace Domain.Entities
{
  public class DigService : IDigService
  {
    public DigResult Dig(GameGrid grid, int px, int py)
    {
      if (!grid.Contains(px, py))
        return DigResult.Fail();

      var cell = grid.GetCell(px, py);

      cell.Break();

      return DigResult.SuccessResult(cell);
    }
  }
}
