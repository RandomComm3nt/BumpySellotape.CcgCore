using System.Collections;
using UnityEngine;

namespace DreamCcg.Controller.Field
{
    public class CardDeckEngineConfig
    {
        public bool CycleDiscardIntoDeck { get; set; } = true;
        public bool ShuffleOnCycle { get; set; } = true;
        public int MaxHandSize { get; set; } = 9;
    }
}