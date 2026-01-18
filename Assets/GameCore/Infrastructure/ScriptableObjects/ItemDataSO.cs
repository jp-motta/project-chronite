// Infrastructure/ScriptableObjects/ItemDataSO.cs
using UnityEngine;
using Domain.Entities;

namespace Infrastructure.ScriptableObjects
{
  [System.Serializable]
  public class ItemDataSO : ScriptableObject
  {
    [SerializeField] private string itemId;
    [SerializeField] private string itemName;
    [SerializeField] private string description;

    public string ItemId => itemId;
    public string ItemName => itemName;
    public string Description => description;

    public Item ToDomain()
    {
      return new Item(itemId, itemName, description);
    }
  }
}
