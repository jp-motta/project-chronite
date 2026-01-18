// Infrastructure/ScriptableObjects/ItemDatabaseSO.cs
using UnityEngine;
using System.Collections.Generic;

namespace Infrastructure.ScriptableObjects
{
  [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Items/Item Database")]
  public class ItemDatabaseSO : ScriptableObject
  {
    [SerializeField] private List<ItemDataSO> items = new();

    public IReadOnlyList<ItemDataSO> Items => items.AsReadOnly();

    private void OnValidate()
    {
      if (items == null)
        items = new List<ItemDataSO>();
    }
  }
}
