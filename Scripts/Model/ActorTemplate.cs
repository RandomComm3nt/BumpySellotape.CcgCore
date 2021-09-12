using BumpySellotape.Core.Stats.Model;
using BumpySellotape.TurnBased.Controller.Actors;
using CcgCore.Model.Cards;
using CcgCore.Model.Config;
using CcgCore.Model.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CcgCore.Model
{
    [CreateAssetMenu(menuName = "Actors/Actor Template")]
    public class ActorTemplate : SerializedScriptableObject
    {
        [field: SerializeField] public string ActorName { get; private set; }
        [field: SerializeField] public List<ActorTemplateRegion> Regions { get; private set; } = new List<ActorTemplateRegion>();
        [field: SerializeField] public List<GeneratedStatTemplate> StatTemplates { get; private set; } = new List<GeneratedStatTemplate>();
        [field: OdinSerialize] public List<TriggeredEffect> TriggeredEffects { get; private set; } = new List<TriggeredEffect>();
        [field: OdinSerialize] public ActorBehaviour ActorBehaviour { get; private set; }
    }

    [Serializable]
    public class ActorTemplateRegion
    {
        [field: SerializeField] public FieldTemplateScope TemplateScope { get; private set; }
        [field: SerializeField, ValueDropdown("@CcgCore.Controller.CardGameEditor.GetAllCards")] public List<CardDefinition> Cards { get; private set; } = new List<CardDefinition>();
    }
}