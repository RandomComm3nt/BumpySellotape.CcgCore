using CcgCore.Controller.Cards;
using TMPro;
using UnityEngine;

namespace CcgCore.View
{
    public class CardDisplayBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardNameText = null;

        public Card Card { get; private set; }

        public virtual void Initialise(Card card)
        {
            Card = card;
            UpdateDisplay();
        }

        protected virtual void UpdateDisplay()
        {
            cardNameText.text = Card.CardDefinition.name;
        }
    }
}
