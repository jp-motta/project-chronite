using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Application.UseCases;
using Application.Interfaces;
using System.Diagnostics;
using Domain.Entities;

public class GameTesterUI : MonoBehaviour
{
  [Header("Repos")]
  [SerializeField] private SOCardRepository cardRepo;
  [SerializeField] private CardUIMapper uiMapper;

  [Header("UI")]
  [SerializeField] private TMP_Text deckCountText;
  [SerializeField] private TMP_Text discardCountText;
  [SerializeField] private Transform handContainer;
  [SerializeField] private GameObject cardViewPrefab;

  [Header("Buttons")]
  [SerializeField] private Button drawButton;

  private IDeckRepository deckRepo;
  private IHandRepository handRepo;

  private DrawCardUseCase drawCardUseCase;
  private UseCardUseCase useCardUseCase;

  public CardDatabaseSO database;

  void Start()
  {
    deckRepo = new DeckRepository(database);
    handRepo = new HandRepository(3);

    var deck = deckRepo.Load();

    drawCardUseCase = new DrawCardUseCase(deckRepo, handRepo);
    useCardUseCase = new UseCardUseCase(deckRepo, handRepo);

    drawButton.onClick.AddListener(OnDrawClicked);

    RefreshUI();
  }

  void OnDrawClicked()
  {
    drawCardUseCase.Execute();
    RefreshUI();
  }

  private void OnCardClicked(Card card)
  {
    useCardUseCase.Execute(card);
    RefreshUI();
  }

  void RefreshUI()
  {
    var deck = deckRepo.Load();
    var hand = handRepo.Load();

    deckCountText.text = $"Deck: {deck.DrawPileCount}";
    discardCountText.text = $"Discard: {deck.DiscardPileCount}";

    // limpa a mão
    foreach (Transform child in handContainer)
      Destroy(child.gameObject);

    // recria visualmente
    foreach (var card in hand.Cards)
    {
      var go = Instantiate(cardViewPrefab, handContainer);
      var view = go.GetComponent<CardView>();
      view.Setup(card, uiMapper.GetArtwork(card));

      view.OnClicked = OnCardClicked;
    }
  }
}
