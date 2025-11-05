using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Game/Cards/Card Database")]
public class CardDatabaseSO : ScriptableObject
{
  public List<CardDataSO> Cards;
}
