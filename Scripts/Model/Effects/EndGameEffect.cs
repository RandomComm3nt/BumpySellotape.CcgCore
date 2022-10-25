using BumpySellotape.Events.Model.Effects;
using CcgCore.Controller;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BumpySellotape.CcgCore.CcgCore.Model.Effects
{
    public class EndGameEffect : IEffect
    {
        [SerializeField, ReadOnly] private bool endImmediately;

        public string Label => "End Game";

        public void Process(ProcessingContext processingContext)
        {
            processingContext.SystemLinks.GetSystemSafe<CardGameControllerBase>().EndGame();
        }
    }
}
