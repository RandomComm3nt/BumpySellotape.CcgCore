using CcgCore.Controller.Cards;
using CcgCore.Model;
using TMPro;
using UnityEngine;

namespace CcgCore.View
{
    public class CardDisplayBase<TCard, TCardDefinition> : MonoBehaviour 
        where TCardDefinition : CardDefinitionBase
        where TCard : CardBase<TCardDefinition>
    {
        [SerializeField] private TextMeshProUGUI cardNameText = null;

        public TCard Card { get; private set; }

        public virtual void Initialise(TCard card)
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
