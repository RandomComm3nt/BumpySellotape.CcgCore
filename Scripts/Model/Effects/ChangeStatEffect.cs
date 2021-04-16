using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using Stats.Model;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    public class ChangeStatEffect : TargetedCardEffect
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] private StatType statType;
        [SerializeField, FoldoutGroup("@DisplayLabel"), LabelText("Change by")] private float changeDelta = 0f;

        public override void ActivateEffects(CardEffectActivationContext context, ParameterScope thisScope)
        {
            var targets = GetTargetActors(context, thisScope);
            foreach (var actor in targets)
            {
                if (actor.StatCollection.GetStat(statType, out var stat))
                {
                    stat.ChangeValue(changeDelta);
                }
            }
        }

        public override string DisplayLabel => $"Change {(statType ? statType.DisplayName : "[stat]")} by {changeDelta}";
    }
}