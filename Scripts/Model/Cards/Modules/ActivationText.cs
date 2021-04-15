﻿using CcgCore.Model.Effects;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model.Cards.Modules
{
    public class ActivationText : CardDefinitionModule
    {
        [SerializeField, FoldoutGroup("Activation Text"), HideReferenceObjectPicker] private List<TextOption> context = new List<TextOption>();
        [SerializeField, FoldoutGroup("Activation Text"), HideReferenceObjectPicker] private List<ActionTextOption> action = new List<ActionTextOption>();
        [SerializeField, FoldoutGroup("Activation Text"), HideReferenceObjectPicker] private List<TextOption> flavour = new List<TextOption>();

        private class TextOption
        {
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public List<TriggerCondition> Conditions { get; private set; } = new List<TriggerCondition>();
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public string Text { get; private set; }
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public float Priority { get; private set; }

            private string DisplayLabel => Text.Substring(0, 100) + (Text.Length > 100 ? "..." : "");
        }

        private class ActionTextOption
        {
            [field: SerializeField, FoldoutGroup("@DisplayLabel")] public bool AddFlavourText { get; private set; }
        }
    }
}