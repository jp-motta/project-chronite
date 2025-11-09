using Application.Interfaces;
using Domain.Entities;

public class HandRepository : IHandRepository
{
  private Hand _hand;
  private readonly int maxSize;

  public HandRepository(int maxSize)
  {
    this.maxSize = maxSize;
  }

  public Hand Load()
  {
    if (_hand == null)
      _hand = new Hand(maxSize);

    return _hand;
  }

  public void Save(Hand hand)
  {
    _hand = hand;
  }
}
