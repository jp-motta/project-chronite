namespace Domain.Entities
{
  public class Card
  {
    public string Id { get; }
    public string Name { get; }
    public int PickaxeCost { get; }
    public string ArtworkId { get; }

    public Card(string id, string name, int cost, string artworkId)
    {
      Id = id;
      Name = name;
      PickaxeCost = cost;
      ArtworkId = artworkId;
    }
  }
}
