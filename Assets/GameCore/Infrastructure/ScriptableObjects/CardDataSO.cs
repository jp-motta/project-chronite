using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Game/Cards/Card")]
public class CardDataSO : ScriptableObject
{
  public string Id;
  public string Name;
  public int Cost;

  public Domain.Entities.Card ToDomain()
  {
    return new Domain.Entities.Card(Id, Name, Cost);
  }
}
