using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Game/Cards/Card Database")]
public class CardDatabaseSO : ScriptableObject
{
  [SerializeField] private List<CardDataSO> cards = new();

  public IReadOnlyList<CardDataSO> Cards => cards;
}
