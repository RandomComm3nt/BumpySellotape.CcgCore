using BumpySellotape.Core.Stats.Controller;
using BumpySellotape.Events.Model.Effects;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BumpySellotape.CcgCore.Model.Effects
{
    public class ChangeStatEffectCcg : ChangeStatEffectBase
    {
        [SerializeField, FoldoutGroup("@Label")] private Target target;

        protected override List<StatCollection> GetTargetStatCollections(ProcessingContext context)
        {
            return target switch
            {
                Target.Player => new() { context.SystemLinks.GetSystemSafe<StatCollection>() },
                Target.Self => new List<StatCollection>(),
                Target.Target1 => new List<StatCollection>(),
                _ => new List<StatCollection>(),
            };
        }
    }
}