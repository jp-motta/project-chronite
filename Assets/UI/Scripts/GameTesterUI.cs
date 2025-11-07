using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Application.UseCases;
using Application.Interfaces;
using System.Diagnostics;

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
  [SerializeField] private Button useFirstButton;

  private IDeckRepository deckRepo;
  private IHandRepository handRepo;

  private DrawCardUseCase drawUseCase;
  private UseCardUseCase useUseCase;

  public CardDatabaseSO database;

  void Start()
  {
    deckRepo = new DeckRepository(database);
    handRepo = new HandRepository(3);

    // Inicializa deck
    var deck = deckRepo.Load();
    foreach (var card in cardRepo.LoadAll())
      deck.AddToDraw(card);

    drawUseCase = new DrawCardUseCase(deckRepo, handRepo);
    useUseCase = new UseCardUseCase(deckRepo, handRepo);

    drawButton.onClick.AddListener(OnDrawClicked);
    useFirstButton.onClick.AddListener(OnUseFirstClicked);

    RefreshUI();
  }

  void OnDrawClicked()
  {
    drawUseCase.Execute();
    RefreshUI();
  }

  void OnUseFirstClicked()
  {
    var hand = handRepo.Load();
    if (hand.Cards.Count > 0)
    {
      var card = hand.Cards[0];
      useUseCase.Execute(card);
    }
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
    }
  }
}
