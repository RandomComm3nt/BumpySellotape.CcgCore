using CcgCore.Controller.Cards;
using CcgCore.Model.Effects;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Cards.Modules
{
    public class ActivationText : CardDefinitionModule
    {
        //[SerializeField, FoldoutGroup("Activation Text"), ListDrawerSettings(CustomAddFunction = "AddTextOption"), HideReferenceObjectPicker] private List<TextOption> context = new List<TextOption>();
        [SerializeField, FoldoutGroup("Activation Text"), ListDrawerSettings(CustomAddFunction = "AddActionTextOption"), HideReferenceObjectPicker] private List<ActionTextOption> action = new List<ActionTextOption>();
        [SerializeField, FoldoutGroup("Activation Text"), ListDrawerSettings(CustomAddFunction = "AddTextOption"), HideReferenceObjectPicker] private List<TextOption> flavour = new List<TextOption>();

        public override void ActivateCard(CardEffectActivationContext context, Card thisCard)
        {
            context.cardGameController.OutputMessage(CreateOutputText());
        }

        private string CreateOutputText()
        {
            //var validContexts = context.Where(o => true);
            var validActions = action.Where(o => true);
            var validFlavours = flavour.Where(o => true);

            var actionWithoutFlavour = validActions.Where(o => !o.AddFlavourText).FirstOrDefault();
            var actionWithFlavour = validActions.Where(o => o.AddFlavourText).FirstOrDefault();
            var chosenFlavour = validFlavours.FirstOrDefault();

            if (actionWithoutFlavour == null)
            {
                if (actionWithFlavour == null)
                    throw new System.Exception();
                return CreateOutputFromOptions(actionWithFlavour, chosenFlavour);
            }
            else
            {
                if (actionWithFlavour == null || chosenFlavour == null)
                    return CreateOutputFromOptions(actionWithoutFlavour);
                return actionWithFlavour.Priority + chosenFlavour.Priority > actionWithoutFlavour.Priority
                    ? CreateOutputFromOptions(actionWithFlavour, chosenFlavour)
                    : CreateOutputFromOptions(actionWithoutFlavour);
            }
        }

        private string CreateOutputFromOptions(TextOption a, TextOption f = null)
        {
            return a.Text;
        }

        [Button, FoldoutGroup("Activation Text")]
        private void LogAllCombinations()
        {/*
            foreach (var (a, b) in context.SelectMany(a => action.Select(b => (a, b))))
            {
                if (!b.AddFlavourText)
                {
                    Debug.Log(CreateOutputFromOptions(a, b));
                    continue;
                }
            }
            */
        }

        private TextOption AddTextOption()
        {
            return new TextOption();
        }

        private ActionTextOption AddActionTextOption()
        {
            return new ActionTextOption();
        }

        private class TextOption
        {
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public List<TriggerCondition> Conditions { get; private set; } = new List<TriggerCondition>();
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public string Text { get; private set; } = "";
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public float Priority { get; private set; }

            private string DisplayLabel => Text.Length > 70 ? Text.Substring(0, 67) + "..." : Text;
        }

        private class ActionTextOption : TextOption
        {
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public bool AddFlavourText { get; private set; }
        }
    }
}