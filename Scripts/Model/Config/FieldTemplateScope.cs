using CcgCore.Model.Parameters;
using UnityEngine;

namespace CcgCore.Model.Config
{
    [CreateAssetMenu(menuName = "Config/Field Scope")]
    public class FieldTemplateScope : ScriptableObject
    {
        [SerializeField] private ParameterScopeLevel parentScopeLevel;
        [SerializeField] private ParameterScopeLevel scopeLevel = ParameterScopeLevel.Region;
    }
}
