using CcgCore.Model.Cards;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.View.Test
{
    public class CardDatabaseList : MonoBehaviour
    {
        public CardDatabaseListItem cardDisplayPrefab;
        public RectTransform cardDisplayParent;
        public TMP_InputField filterInput;
        public Action<CardDefinition> clickAction;

        private List<CardDatabaseListItem> cardDisplays;

        [ValueDropdown("@CcgCore.Controller.CardGameEditor.CardGameConfig.CardTags")]
        public List<int> tagsToInclude;

        [SerializeField] private string searchPathFilter = "";

        public void Start()
        {
            cardDisplays = AssetDatabase.FindAssets("t:CardDefinition")
                .Select(s => AssetDatabase.GUIDToAssetPath(s))
                .Where(s => s.Contains(searchPathFilter))
                .Select(s => AssetDatabase.LoadAssetAtPath<CardDefinition>(s))
                .Where(cd => !cd.DisableCard)
                .Where(cd => tagsToInclude.Count == 0 || cd.Tags.Intersect(tagsToInclude).Count() > 0)
                .Select(cd =>
                {
                    var cardDisplay = Instantiate(cardDisplayPrefab, cardDisplayParent);
                    cardDisplay.Initialise(this, cd);
                    return cardDisplay;
                }).ToList();

            filterInput.onValueChanged.AddListener(s => OnFilterChange());
        }

        public void OnFilterChange()
        {
            cardDisplays.ForEach(cd => cd.gameObject.SetActive(filterInput.text.Length == 0 || cd.CardDefinition.name.ToLower().Contains(filterInput.text.ToLower())));
        }

        public void OnItemClicked(CardDefinition cardDefinition)
        {
            clickAction?.Invoke(cardDefinition);
        }
    }
}
