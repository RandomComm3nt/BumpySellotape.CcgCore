using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Effects.Conditions
{
    public class CardExistsCondition : BumpySellotape.CcgCore.Model.Effects.Conditions.Condition
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] private bool invertCondition;
        [SerializeField, HideReferenceObjectPicker, FoldoutGroup("@DisplayLabel"), HideLabel] private CardCondition cardCondition = new CardCondition();

        public override bool CheckCondition(ParameterScope scope)
        {
            var scopes = scope.GetAllChildScopesAtLevel(ParameterScopeLevel.Card);
            return invertCondition 
                ? scopes.All(c => !cardCondition.CheckCondition(c)) 
                : scopes.Any(c => cardCondition.CheckCondition(c));
        }

        #region Editor
        public override string DisplayLabel
        { 
            get
            {
                var name = invertCondition ? "Card Doesn't Exist In Scope" : "Card Exists In Scope";
                var conditionName = cardCondition.DisplayLabel;
                if (!string.IsNullOrEmpty(conditionName))
                    name += " - " + conditionName;
                return name;
            } 
        }
        #endregion
    }
}
