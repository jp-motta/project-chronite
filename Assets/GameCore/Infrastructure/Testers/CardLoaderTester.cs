using UnityEngine;
using Application.UseCases;

public class CardLoaderTester : MonoBehaviour
{
  [SerializeField] private SOCardRepository cardRepository;

  private void Start()
  {
    var usecase = new LoadAllCardsUseCase(cardRepository);

    var cards = usecase.Execute();

    Debug.Log("=== Cartas carregadas ===");
    foreach (var card in cards)
    {
      Debug.Log($"{card.Id} - {card.Name} (Custo: {card.PickaxeCost})");
    }
  }
}
