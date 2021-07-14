using BumpySellotape.Events.Model.Actions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CcgCore.Model.Cards
{
    [CreateAssetMenu(menuName = "Card/Card Definition")]
    public class CardDefinition : SerializedScriptableObject
    {
        [field: SerializeField, ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")]
        public List<int> Tags { get; private set; } = new List<int>();

        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        [field: SerializeField] 
        public List<CardDefinition> PossibleTargets { get; private set; } = new List<CardDefinition>();
        [field: SerializeField] public Sprite Sprite { get; private set; }


        [field: OdinSerialize, HideReferenceObjectPicker] public List<CardDefinitionModule> Modules { get; private set; } = new List<CardDefinitionModule>();
        [field: SerializeField, HorizontalGroup("Metadata")] public bool DebugCard { get; private set; }
        [field: SerializeField, HorizontalGroup("Metadata")] public bool DisableCard { get; private set; }

        public T GetModule<T>() where T : CardDefinitionModule
        {
            return Modules.FirstOrDefault(m => m.GetType() == typeof(T)) as T;
        }

#if UNITY_EDITOR
        [Button, ShowInInspector]
        private void ConvertToAction()
        {
            var a = CreateInstance<ActionDefinition>();
            a.name = name;

            a.GetType().GetProperty(nameof(a.Description)).SetValue(a, Description);
            
            a.GetType().GetProperty(nameof(a.DebugAction)).SetValue(a, DebugCard);
            a.GetType().GetProperty(nameof(a.DisableAction)).SetValue(a, DisableCard);

            var path = AssetDatabase.GetAssetPath(this);
            var parentFolder = path.Substring(0, path.IndexOf(name));
            if (!AssetDatabase.IsValidFolder(parentFolder + "Archive"))
            {
                Debug.Log("Creating folder: " + parentFolder);
                var guid = AssetDatabase.CreateFolder(parentFolder.Substring(0, parentFolder.Length - 1), "Archive");
            }
            var e = AssetDatabase.MoveAsset(path, path.Replace(name, "Archive/" + name));
            if (string.IsNullOrEmpty(e))
            {
                AssetDatabase.CreateAsset(a, path);
            }
            else
                Debug.LogWarning(e);
        }
#endif
    }
}