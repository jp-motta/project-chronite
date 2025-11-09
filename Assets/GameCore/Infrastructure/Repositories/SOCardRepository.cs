using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Entities;
using UnityEngine;

public class SOCardRepository : MonoBehaviour, ICardRepository
{
  [SerializeField] private CardDatabaseSO database;

  public List<Card> LoadAll()
  {
    return database.Cards
        .Select(c => c.ToDomain())
        .ToList();
  }

  public Card? GetById(string id)
  {
    var so = database.Cards.FirstOrDefault(c => c.Id == id);
    return so?.ToDomain();
  }
}
