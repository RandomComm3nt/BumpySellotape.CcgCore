using CcgCore.Controller.Cards;
using CcgCore.Model.Cards;
using CcgCore.Model.Config;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class AddCardsEffect : TargetedCardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] private RegionSelectionType regionSelectionType;
        [SerializeField, FoldoutGroup("@DisplayLabel"), ShowIf("regionSelectionType", RegionSelectionType.ByTemplate)] private FieldTemplateScope regionTemplate;
        [SerializeField, FoldoutGroup("@DisplayLabel"), ValueDropdown("@CcgCore.Controller.CardGameEditor.GetAllCards")] private List<CardDefinition> cards = new List<CardDefinition>();

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope)
        {
            var targets = GetTargetActors(context, thisScope);
            foreach (var actor in targets)
            {
                var scopes = actor.ActorScope.GetAllChildScopesAtLevel(Parameters.ParameterScopeLevel.Region).Select(s => s as FieldRegion).ToList();
                var regions = regionSelectionType switch
                {
                    RegionSelectionType.All => scopes,
                    RegionSelectionType.Random => scopes.GetRange(Random.Range(0, scopes.Count), 1),
                    RegionSelectionType.ByTemplate => scopes.Where(r => r.TemplateScope == regionTemplate),
                    _ => throw new System.NotImplementedException(),
                };
                foreach (var region in regions)
                {
                    foreach (var card in cards)
                        region.AddCard(card);
                }
            }
        }

        private enum RegionSelectionType
        {
            All = 0,
            Random,
            ByTemplate,
        }

        public override string DisplayLabel => cards.Count == 1 && cards[0] ? $"Add card {cards[0].name} to {TargetString}" : $"Add {cards.Count} cards to {TargetString}";
    }
}
