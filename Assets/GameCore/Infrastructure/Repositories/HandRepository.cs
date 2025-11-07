using Application.Interfaces;
using Domain.Entities;

public class HandRepository : IHandRepository
{
  private readonly int maxSize;

  public HandRepository(int size)
  {
    maxSize = size;
  }

  public Hand Load()
  {
    return new Hand(maxSize);
  }

  public void Save(Hand hand)
  {
    // Futuro: salvar estado? Por enquanto não é necessário.
  }
}
