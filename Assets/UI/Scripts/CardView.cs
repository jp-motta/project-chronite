using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Domain.Entities;

public class CardView : MonoBehaviour
{
  [SerializeField] private Image artwork;
  [SerializeField] private TMP_Text nameText;
  [SerializeField] private TMP_Text costText;

  private Card card;

  public Card Card => card;

  public void Setup(Card card, Sprite sprite)
  {
    this.card = card;

    artwork.sprite = sprite;
    nameText.text = card.Name;
    costText.text = "Cost: " + card.PickaxeCost;
  }
}
