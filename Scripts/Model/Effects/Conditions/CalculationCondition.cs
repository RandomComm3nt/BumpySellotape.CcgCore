using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace CcgCore.Model.Effects.Conditions
{
    [Serializable]
    public class CalculationCondition
    {
        [SerializeField, LabelText("If a random number up to")] private float randomFactor = 0f;
        [SerializeField, LabelText("Is >=")] private float successThreshold = 0f;

        public bool CheckCondition()
        {
            return UnityEngine.Random.Range(0f, randomFactor) > successThreshold;
        }
    }
}
