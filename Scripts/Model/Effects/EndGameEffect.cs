using BumpySellotape.Events.Model.Effects;
using CcgCore.Model;
using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BumpySellotape.CcgCore.CcgCore.Model.Effects
{
    public class EndGameEffect : IEffect
    {
        [SerializeField] private bool endImmediately;

        public string Label => "End Game";

        public void Process(ProcessingContext processingContext)
        {
            throw new NotImplementedException();
        }
    }
}
