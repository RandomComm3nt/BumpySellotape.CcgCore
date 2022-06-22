using System.Collections;
using UnityEngine;

namespace DreamCcg.Controller.Field
{
    public class CardDeckEngineConfig
    {
        public bool CycleDiscardIntoDeck { get; } = true;
        public bool ShuffleOnCycle { get; } = true;
        public int MaxHandSize { get; } = 9;
    }
}