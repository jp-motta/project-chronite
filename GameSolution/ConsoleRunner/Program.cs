// ...existing code...
using System;
using System.Linq;
using Application;
using Domain.Entities;

class Program
{
  static void Main(string[] args)
  {
    var deck = new Deck();
    var hand = new Hand(3);
    var service = new DeckService(deck, hand);

    // adicionar cartas ao deck
    deck.AddToDraw(new Card("c1", "Sala Normal", 2));
    deck.AddToDraw(new Card("c2", "Sala Estreita", 1));
    deck.AddToDraw(new Card("c3", "Sala Perigosa", 3));

    Console.WriteLine("---- TESTE DE COMPRAR CARTAS ----");

    for (int i = 0; i < 4; i++)
    {
      var card = service.DrawCard();
      if (card != null)
        Console.WriteLine($"Comprou a carta: {card.Name}");
      else
        Console.WriteLine("Sem cartas para comprar!");
    }

    Console.WriteLine("\n---- CARTAS NA MÃO ----");
    foreach (var c in hand.Cards)
      Console.WriteLine(c.Name);

    Console.WriteLine("\n---- TESTE DE USAR CARTA ----");
    var first = hand.Cards.First();
    service.UseCard(first);

    Console.WriteLine("Usou " + first.Name);

    Console.WriteLine("\n---- CARTAS NA MÃO AGORA ----");
    foreach (var c in hand.Cards)
      Console.WriteLine(c.Name);
  }
}
// ...existing code...
