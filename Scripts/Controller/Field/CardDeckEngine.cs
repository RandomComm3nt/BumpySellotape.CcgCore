using BumpySellotape.CcgCore.Controller.Cards.States;
using BumpySellotape.Core.Utilities;
using CcgCore.Controller;
using CcgCore.Controller.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace DreamCcg.Controller.Field
{
    public class CardDeckEngine
    {
        public enum Region
        {
            Deck,
            Hand,
            Discard,
            Trash,
        }

        public List<Card> Deck { get; private set; }
        public List<Card> Hand { get; private set; } = new();
        public List<Card> DiscardPile { get; private set; } = new();
        public List<Card> TrashPile { get; private set; } = new();

        public CardGameControllerBase CardGameController { get; } 
        public CardDeckEngineConfig Config { get; }

        public delegate void MovedCardHandler(Region fromRegion, Region toRegion, Card card);
        public event MovedCardHandler? MovedCard;

        public CardDeckEngine(CardDeckEngineConfig config, CardGameControllerBase cardGameController, List<Card> deck)
        {
            Config = config;
            CardGameController = cardGameController;
            Deck = deck;
            Deck.ForEach(c => c.CardState = new DeckState(this, c));
        }

        public void DrawCard(Card card)
        {
            if (Hand.Count >= Config.MaxHandSize)
                throw new Exception($"Tried to draw card but max hand size has been reached");

            MoveCard(Region.Deck, Region.Hand, card);
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
                    CycleDiscardPileToDeck();
                    DrawTopCard();
                }
            }
            else
            {
                DrawCard(Deck[0]);
            }
        }

        public void CycleDiscardPileToDeck()
        {
            DiscardPile.ToList().ForEach(c => MoveCard(Region.Discard, Region.Deck, c));
            if (Config.ShuffleOnCycle)
            {
                ShuffleDeck();
            }
        }

        public void DrawUpTo(int handSize)
        {
            if (Hand.Count >= handSize)
                return;

            DrawTopCards(handSize - Hand.Count);
        }

        public void DiscardCard(Card card)
        {
            MoveCard(Region.Hand, Region.Discard, card);
        }

        public void DiscardCardFromDeck(Card card)
        {
            MoveCard(Region.Deck, Region.Discard, card);
        }

        public void DiscardHand()
        {
            while (Hand.Count > 0)
                DiscardCard(Hand[0]);
        }

        public void MillTopCard()
        {
            throw new NotImplementedException();
        }

        public void TrashCardFromHand(Card card)
        {
            MoveCard(Region.Hand, Region.Trash, card);
        }

        public void ShuffleDeck()
        {
            Deck = Deck.Shuffle();
        }

        public void PlayCard(Card card)
        {
            var context = CardGameController.CreateContext();
            VerifyCardInList(Hand, card);

            /*
            Hand.Remove(card);
            card.PlayCard(context);
            DiscardPile.Add(card);
            */
            //TODO: This should raise its own event so UI can make it clear card was played
            // should also activate while card is not in region, as above

            MoveCard(Region.Hand, Region.Discard, card);
            card.PlayCard(context);
        }

        private void MoveCard(Region fromRegion, Region toRegion, Card card)
        {
            var fromList = GetRegionList(fromRegion);
            var toList = GetRegionList(toRegion);
            VerifyCardInList(fromList, card);
            fromList.Remove(card);
            toList.Add(card);
            card.CardState = new TransitioningState(this, card);
            MovedCard?.Invoke(fromRegion, toRegion, card);
        }

        private List<Card> GetRegionList(Region region)
        {
            return region switch
            {
                Region.Deck => Deck,
                Region.Hand => Hand,
                Region.Discard => DiscardPile,
                Region.Trash => TrashPile,
                _ => throw new NotImplementedException(),
            };
        }

        private void VerifyCardInList(List<Card> list, Card card)
        {
            if (list.IndexOf(card) < 0)
                throw new Exception($"Card {card.CardDefinition} was not found in target region");
        }

        public void AcquireCard(Region toRegion, Card card)
        {
            GetRegionList(toRegion).Add(card);
        }

        public void RemoveCard(Region fromRegion, Card card)
        {
            GetRegionList(fromRegion).Remove(card);
        }
    }
}