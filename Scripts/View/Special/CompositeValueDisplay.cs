using CcgCore.Controller.Cards;
using CcgCore.Model.Special;
using TMPro;
using UnityEngine;

namespace CcgCore.View.Special
{
    public abstract class CompositeValueDisplay<TCard> : MonoBehaviour
        where TCard : Card
    {
        private FieldRegion fieldRegion;
        [SerializeField] private TextMeshProUGUI valueField = null;
        [SerializeField] private CompositeValue compositeValue = null;

        public void Initialise(FieldRegion region)
        {
            fieldRegion = region;
            fieldRegion.OnCardAdded += UpdateDisplay; // TECH DEBT - should this be a generic on change?
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            valueField.text = compositeValue.GetValue(fieldRegion).ToString();
        }
    }
}
