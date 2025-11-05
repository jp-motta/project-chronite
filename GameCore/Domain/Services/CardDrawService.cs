using Domain.Entities;

namespace Domain.Services
{
    public class CardDrawService
    {
        /// <summary>
        /// Tenta comprar uma carta. Retorna null se a mão estiver cheia ou o deck vazio.
        /// </summary>
        public Card? Draw(Deck deck, Hand hand)
        {
            if (!hand.CanAdd)
                return null;

            var card = deck.Draw();

            if (card != null)
                hand.Add(card);

            return card;
        }
    }
}
