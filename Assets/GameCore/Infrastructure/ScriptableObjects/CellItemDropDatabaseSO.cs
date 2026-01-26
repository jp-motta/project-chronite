// Infrastructure/ScriptableObjects/CellItemDropDatabaseSO.cs
using UnityEngine;
using System.Collections.Generic;
using Domain.Entities;

namespace Infrastructure.ScriptableObjects
{
  [System.Serializable]
  public class CellItemDrop
  {
    public FeatureType featureType;
    public ItemDataSO itemDrop;
  }

  [CreateAssetMenu(fileName = "CellItemDropDatabase", menuName = "Game/Cell Item Drop Database")]
  public class CellItemDropDatabaseSO : ScriptableObject
  {
    [SerializeField] private List<CellItemDrop> drops = new();

    public ItemDataSO GetDropForFeature(FeatureType feature)
    {
      var drop = drops.Find(d => d.featureType == feature);
      return drop?.itemDrop;
    }
  }
}
