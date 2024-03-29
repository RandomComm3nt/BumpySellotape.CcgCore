﻿using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CcgCore.Model.Cards
{
    [CreateAssetMenu(menuName = "Card/Card Definition")]
    public class CardDefinition : SerializedScriptableObject
    {
        [SerializeField] private string displayNameOverride;
        [ShowInInspector] public string DisplayName => (displayNameOverride?.Length ?? 0) > 0 ? displayNameOverride : name;

        [field: SerializeField, ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")]
        public List<int> Tags { get; private set; } = new ();

        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        [field: SerializeField] 
        public List<CardDefinition> PossibleTargets { get; private set; } = new ();
        [field: SerializeField] public Sprite Sprite { get; private set; }


        [field: OdinSerialize, HideReferenceObjectPicker] public List<CardDefinitionModule> Modules { get; private set; } = new();
        [field: SerializeField, HorizontalGroup("Metadata")] public bool DebugCard { get; private set; }
        [field: SerializeField, HorizontalGroup("Metadata")] public bool DisableCard { get; private set; }

        public T GetModule<T>() where T : CardDefinitionModule
        {
            return Modules.FirstOrDefault(m => m.GetType() == typeof(T)) as T;
        }
    }
}