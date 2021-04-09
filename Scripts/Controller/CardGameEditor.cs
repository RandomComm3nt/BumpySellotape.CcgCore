using CcgCore.Model.Cards;
using CcgCore.Model.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace CcgCore.Controller
{
#if UNITY_EDITOR
    public static class CardGameEditor
    {
        private static CardGameConfig cardGameConfig;

        public static CardGameConfig CardGameConfig
        {
            get
            {
                if (!cardGameConfig)
                {
                    var guids = AssetDatabase.FindAssets($"t:{typeof(CardGameConfig).Name}");
                    cardGameConfig = AssetDatabase.LoadAssetAtPath<CardGameConfig>(AssetDatabase.GUIDToAssetPath(guids[0]));
                }
                return cardGameConfig;
            }
            set
            {
                cardGameConfig = value;
            }
        }

        public static List<CardDefinition> GetAllCards => AssetDatabase
            .FindAssets($"t:{typeof(CardDefinition).Name}")
            .Select(s => AssetDatabase.GUIDToAssetPath(s))
            .Where(s => s.Contains(CardGameConfig.CardPathFilter))
            .Select(s => AssetDatabase.LoadAssetAtPath<CardDefinition>(s))
            .ToList();
    }
#endif
}
