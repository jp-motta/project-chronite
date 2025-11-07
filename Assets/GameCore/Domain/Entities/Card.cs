using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public class Card
  {
    public string Id { get; }
    public string Name { get; private set; }
    public int PickaxeCost { get; private set; }
    public string ArtworkId { get; private set; }

    public Card(string id, string name, int cost, string artworkId)
    {
      Id = id;
      Name = name;
      PickaxeCost = cost;
      ArtworkId = artworkId;
    }
  }
}
