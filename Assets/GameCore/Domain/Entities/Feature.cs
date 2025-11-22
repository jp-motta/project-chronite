namespace Domain.Entities
{
  public enum FeatureType
  {
    Empty,
    Ore
  }

  public class Feature
  {
    public FeatureType Type;
    public int Value;
  }
}
