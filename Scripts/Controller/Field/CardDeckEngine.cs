using BumpySellotape.Core.Utilities;
using CcgCore.Controller.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

namespace DreamCcg.Controller.Field
{
    public class CardDeckEngine
    {
        public List<Card> Deck { get; private set; }
        public List<Card> Hand { get; private set; } = new();
        public List<Card> DiscardPile { get; private set; } = new();
        public List<Card> TrashPile { get; private set; } = new();
        public CardDeckEngineConfig Config { get; }

        public CardDeckEngine(CardDeckEngineConfig config, List<Card> deck)
        {
            Config = config;
            Deck = deck;
        }

        public void DrawCard(Card card)
        {
            if (Deck.IndexOf(card) < 0)
                throw new Exception($"Tried to explicitly draw card {card.CardDefinition} but was not in deck");
            if (Hand.Count >= Config.MaxHandSize)
                throw new Exception($"Tried to draw card but max hand size has been reached");

            Debug.Log($"Drew {card.CardDefinition.name}");

            Deck.Remove(card);
            Hand.Add(card);
        }

        public void DrawTopCards(int cardsToDraw)
        {
            for (int i = 0; i < cardsToDraw; i++)
            {
                DrawTopCard();
            }
        }

        public void DrawTopCard()
        {
            if (Deck.Count == 0)
            {
                if (DiscardPile.Count > 0 && Config.CycleDiscardIntoDeck)
                {
                    Deck = DiscardPile.ToList();
                    DiscardPile = new();
                    if (Config.ShuffleOnCycle)
                    {
                        ShuffleDeck();
                    }
                    DrawTopCard();
                }
            }
            else
            {
                DrawCard(Deck[0]);
            }
        }

        public void DiscardCard(Card card)
        {
            if (Hand.IndexOf(card) < 0)
                throw new Exception($"Tried to explicitly discard card {card.CardDefinition} but was not in hand");

            Hand.Remove(card);
            DiscardPile.Add(card);
        }

        public void DiscardCardFromDeck(Card card)
        {
            throw new NotImplementedException();
        }

        public void MillTopCard()
        {
            throw new NotImplementedException();
        }

        public void ShuffleDeck()
        {
            Deck = Deck.Shuffle();
        }
    }
}