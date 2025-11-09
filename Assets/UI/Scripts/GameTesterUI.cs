using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Application.UseCases;
using Application.Interfaces;
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

  private readonly List<CardView> cardViewPool = new();

  void Start()
  {
    deckRepo = new DeckRepository(database);
    handRepo = new HandRepository(3);

    drawCardUseCase = new DrawCardUseCase(deckRepo, handRepo);
    useCardUseCase = new UseCardUseCase(deckRepo, handRepo);

    drawButton.onClick.AddListener(OnDrawClicked);

    RefreshUI();
  }

  void OnDrawClicked()
  {
    var result = drawCardUseCase.Execute();
    if (!result.IsSuccess)
      Debug.LogWarning($"[GameTesterUI] Draw failed: {result.Error}");

    RefreshUI();
  }

  private void OnCardClicked(Card card)
  {
    var result = useCardUseCase.Execute(card);
    if (!result.IsSuccess)
      Debug.LogWarning($"[GameTesterUI] Use failed: {result.Error}");

    RefreshUI();
  }

  void RefreshUI()
  {
    var deck = deckRepo.Load();
    var hand = handRepo.Load();

    deckCountText.text = $"Deck: {deck.DrawPileCount}";
    discardCountText.text = $"Discard: {deck.DiscardPileCount}";

    int i = 0;
    foreach (var card in hand.Cards)
    {
      CardView view;
      if (i < cardViewPool.Count)
      {
        view = cardViewPool[i];
        view.gameObject.SetActive(true);
      }
      else
      {
        var go = Instantiate(cardViewPrefab, handContainer);
        view = go.GetComponent<CardView>();
        cardViewPool.Add(view);
      }

      view.Setup(card, uiMapper.GetArtwork(card));
      view.OnClicked = OnCardClicked;
      i++;
    }

    for (; i < cardViewPool.Count; i++)
      cardViewPool[i].gameObject.SetActive(false);
  }
}
