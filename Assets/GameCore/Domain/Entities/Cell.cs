namespace Domain.Entities
{
  public enum FeatureType
  {
    Empty = 0,
    Ore = 1
  }

  public class Cell
  {
    public int X { get; }
    public int Y { get; }
    public FeatureType Feature { get; private set; }

    public Cell(int x, int y, FeatureType feature = FeatureType.Empty)
    {
      X = x;
      Y = y;
      Feature = feature;
    }

    public void SetFeature(FeatureType feature) => Feature = feature;

    public bool Break() => true;
  }
}
