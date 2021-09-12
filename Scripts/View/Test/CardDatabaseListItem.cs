using CcgCore.Model.Cards;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.View.Test
{
    public class CardDatabaseListItem : MonoBehaviour
    {
        private CardDatabaseList cardDatabaseList;
        public CardDefinition CardDefinition { get; set; }

        public TextMeshProUGUI label;

        public void Initialise(CardDatabaseList cardDatabaseList, CardDefinition cd)
        {
            CardDefinition = cd;
            label.text = cd.name;
            this.cardDatabaseList = cardDatabaseList;
        }

        public void Click()
        {
            cardDatabaseList.OnItemClicked(CardDefinition);
        }
    }
}
