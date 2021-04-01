using CcgCore.Model;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Model.Effects
{
    [Serializable]
    public class StackProtectionEffect
    {
        [field: SerializeField] public ProtectionDirection Direction { get; private set; } = ProtectionDirection.None;
        [field: SerializeField, ShowIf("HasEffect")] public CardFilter ActionsToProtectFrom { get; private set; }

        public enum ProtectionDirection
        {
            None = 0,
            FullStack,
            Above,
            Below,
        }

        public bool HasEffect => Direction != ProtectionDirection.None;
    }
}
