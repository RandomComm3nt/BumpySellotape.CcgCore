using BumpySellotape.TurnBased.Controller.Actors;
using CcgCore.Model.Effects;
using CcgCore.Model.Effects.Conditions;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Cards
{
    public class CardActivationRequirements : CardDefinitionModule
    {
        [SerializeField, FoldoutGroup("Activation Requirements"), ListDrawerSettings(CustomAddFunction = "AddTriggerCondition")] private List<TriggerCondition> conditions = new List<TriggerCondition>();
        [SerializeField, FoldoutGroup("Activation Requirements"), HideReferenceObjectPicker, ListDrawerSettings(CustomAddFunction = "AddCommonCondition")] private List<CommonCondition> commonConditions = new List<CommonCondition>();
        /*
        public override bool ValidateCardCanBeUsed(Actor actor)
        {
            return conditions
                .Union(commonConditions.Select(cc => cc.CommonConditionDefinition.TriggerCondition))
                .All(c => c.CheckConditions(null, actor.ActorScope));
        }
        */
#if UNITY_EDITOR
        private TriggerCondition AddTriggerCondition() => new TriggerCondition();
        private CommonCondition AddCommonCondition() => new CommonCondition();
#endif
    }
}
