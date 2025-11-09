using UnityEngine;
using UnityEngine.Serialization;
using Domain.Entities;

public class CardDataSO : ScriptableObject
{
  [SerializeField] private string id;

  [FormerlySerializedAs("name")]
  [SerializeField] private string displayName;

  [SerializeField] private int pickaxeCost;
  [SerializeField] private string artworkId;
  [SerializeField] private Sprite artwork;

  public string Id => id;
  public string Name => displayName;
  public int PickaxeCost => pickaxeCost;
  public string ArtworkId => artworkId;
  public Sprite Artwork => artwork;

  public Card ToDomain()
  {
    var effectiveId = string.IsNullOrWhiteSpace(Id) ? name : Id;
    return new Card(effectiveId, Name, PickaxeCost, ArtworkId);
  }
}
