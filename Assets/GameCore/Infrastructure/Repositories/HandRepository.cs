using Application.Interfaces;
using Domain.Entities;

public class HandRepository : IHandRepository
{
  private Hand _hand;

  public HandRepository(int maxSize)
  {
    _hand = new Hand(maxSize);
  }

  public Hand Load() => _hand;

  public void Save(Hand hand)
  {
    _hand = hand;
  }
}
