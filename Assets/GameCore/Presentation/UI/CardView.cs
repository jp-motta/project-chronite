using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Domain.Entities;
using System;

public class CardView : MonoBehaviour
{
  [SerializeField] private Image artwork;
  [SerializeField] private TMP_Text nameText;
  [SerializeField] private TMP_Text costText;
  [SerializeField] private Button button;

  private Card card;

  public Action<Card> OnClicked;

  public void Setup(Card card, Sprite sprite)
  {
    this.card = card;

    artwork.sprite = sprite;
    nameText.text = card.Name;
    costText.text = "Cost: " + card.PickaxeCost;

    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() => OnClicked?.Invoke(card));
  }
}
