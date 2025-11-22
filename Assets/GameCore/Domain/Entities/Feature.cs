namespace Domain.Entities
{
  public enum FeatureType { Loot, Ore, Obstacle }

  public class Feature
  {
    public FeatureType Type;
    public int Value;
  }
}
