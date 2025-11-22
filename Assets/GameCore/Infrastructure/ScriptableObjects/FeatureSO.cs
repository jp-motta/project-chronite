using UnityEngine;
using Domain.Entities;

[CreateAssetMenu(menuName = "Feature")]
public class FeatureSO : ScriptableObject
{
  public FeatureType type;
  public int value;
}
