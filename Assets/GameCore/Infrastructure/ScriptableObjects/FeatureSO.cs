using UnityEngine;

[CreateAssetMenu(menuName = "Feature")]
public class FeatureSO : ScriptableObject
{
  public Domain.Entities.FeatureType type;
  public int value;
}
