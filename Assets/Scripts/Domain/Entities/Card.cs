namespace Domain.Entities
{
  public class Card
  {
    public string Name { get; private set; }
    public int PickaxeCost { get; private set; }

    public Card(string name, int cost, bool[] openPassages)
    {
      Name = name;
      PickaxeCost = cost;
    }
  }
}
