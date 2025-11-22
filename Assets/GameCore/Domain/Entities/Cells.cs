namespace Domain.Entities
{
  public class Cell
  {
    public int X { get; }
    public int Y { get; }

    public bool Explored { get; private set; }
    public Feature[] Features { get; }

    public Cell(int x, int y, Feature[] features)
    {
      X = x;
      Y = y;
      Features = features;
    }

    public void MarkAsExplored()
        => Explored = true;
  }
}
