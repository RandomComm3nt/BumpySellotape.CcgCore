using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace CcgCore.Model.Effects
{
    [Serializable, HideReferenceObjectPicker]
    public class ScopeSelection
    {
        [SerializeField, FoldoutGroup("@DisplayLabel")] public ActivationEffectTargetType target = ActivationEffectTargetType.Activator;

    }
}
