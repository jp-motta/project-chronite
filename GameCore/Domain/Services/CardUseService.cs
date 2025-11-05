using Domain.Entities;

namespace Domain.Services
{
    public class CardUseService
    {
        /// <summary>
        /// Usa uma carta da mão e a envia para a pilha de descarte.
        /// </summary>
        public void Use(Deck deck, Hand hand, Card card)
        {
            if (!hand.Contains(card))
                return;

            hand.Remove(card);
            deck.AddToDiscard(card);
        }
    }
}
