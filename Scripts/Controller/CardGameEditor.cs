using CcgCore.Model;
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
        }
    }
#endif
}
