using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

namespace CcgCore.Model.Effects
{
    public class CommonEffectDefinition : SerializedScriptableObject
    {
        [OdinSerialize] private List<CardEffect> cardEffects;
    }
}
