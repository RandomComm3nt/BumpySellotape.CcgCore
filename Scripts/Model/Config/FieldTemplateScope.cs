using CcgCore.Model.Parameters;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model.Config
{
    [CreateAssetMenu(menuName = "Config/Field Scope")]
    public class FieldTemplateScope : ScriptableObject
    {
        [SerializeField] private List<FieldTemplateScope> childScopes = new();
        [SerializeField] private ParameterScopeLevel scopeLevel = ParameterScopeLevel.Region;
    }
}
