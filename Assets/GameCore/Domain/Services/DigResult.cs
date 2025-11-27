namespace Domain.Entities
{
  public class DigResult
  {
    public bool Success { get; }
    public Cell DugCell { get; }

    private DigResult(bool success, Cell cell)
    {
      Success = success;
      DugCell = cell;
    }

    public static DigResult Fail() => new DigResult(false, null);
    public static DigResult SuccessResult(Cell c) => new DigResult(true, c);
  }
}
