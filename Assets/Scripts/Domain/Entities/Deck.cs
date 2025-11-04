public class Deck
{
  private readonly List<Card> _drawPile = new();
  private readonly List<Card> _discardPile = new();

  public void AddToDraw(Card c) => _drawPile.Add(c);

  public void AddToDiscard(Card c) => _discardPile.Add(c);

  public Card Draw()
  {
    if (_drawPile.Count == 0)
      Reshuffle();

    if (_drawPile.Count == 0)
      return null;

    var c = _drawPile[0];
    _drawPile.RemoveAt(0);
    return c;
  }

  private void Reshuffle()
  {
    _drawPile.AddRange(_discardPile);
    _discardPile.Clear();

    _drawPile = _drawPile.OrderBy(x => Random.Shared.Next()).ToList();
  }
}
