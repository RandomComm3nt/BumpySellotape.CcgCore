using CcgCore.Controller.Cards;
using CcgCore.Model.Parameters;

namespace CcgCore.Controller.Actors
{
    public class ActorFieldSide : ActorScope
    {
        public FieldRegion Deck { get; }
        public FieldRegion Hand { get; }
        public FieldRegion DiscardPile { get; }

        public ActorFieldSide(CardGameControllerBase cardGameController, ParameterScope parentScope = null)
            : base(parentScope ?? cardGameController)
        {
            Deck = new FieldRegion(cardGameController, this);
            Hand = new FieldRegion(cardGameController, this);
            DiscardPile = new FieldRegion(cardGameController, this);
        }

        public void DrawCard(Card card)
        {
            MoveCardFromRegionToRegion(Deck, Hand, card);
        }

        public void DrawTopCard()
        {
            //MoveCardFromRegionToRegion(Deck, Hand, card);
        }

        public void DiscardCard(Card card)
        {
            MoveCardFromRegionToRegion(Hand, DiscardPile, card);
        }

        public void MillTopCard()
        {
            //MoveCardFromRegionToRegion(Deck, Hand, card);
        }

        public void DiscardCardFromDeck(Card card)
        {
            MoveCardFromRegionToRegion(Deck, DiscardPile, card);
        }

        private void MoveCardFromRegionToRegion(FieldRegion sourceRegion, FieldRegion destinationRegion, Card card)
        {

        }
    }
}
