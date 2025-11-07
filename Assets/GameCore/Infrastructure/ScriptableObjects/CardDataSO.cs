using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Game/Cards/Card")]
public class CardDataSO : ScriptableObject
{
  public string Id;
  public string Name;
  public int PickaxeCost;
  public string ArtworkId;
  public Sprite Artwork;

  public Domain.Entities.Card ToDomain()
  {
    return new Domain.Entities.Card(Id, Name, PickaxeCost, ArtworkId);
  }
}
