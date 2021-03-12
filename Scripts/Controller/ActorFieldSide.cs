using CcgCore.Controller.Cards;
using CcgCore.Model;
using CcgCore.Model.Parameters;

namespace CcgCore.Controller
{
    public class ActorFieldSide<TCard, TCardDefinition> : ParameterScope
        where TCard : CardBase<TCardDefinition>
        where TCardDefinition : CardDefinitionBase
    {
        public FieldRegion<TCard, TCardDefinition> Deck { get; }
        public FieldRegion<TCard, TCardDefinition> Hand { get; }
        public FieldRegion<TCard, TCardDefinition> DiscardPile { get; }

        public ActorFieldSide(CardGameControllerBase cardGameController, ParameterScope parentScope = null)
            : base(ParameterScopeLevel.Actor, parentScope ?? cardGameController)
        {
            Deck = new FieldRegion<TCard, TCardDefinition>(cardGameController, this);
            Hand = new FieldRegion<TCard, TCardDefinition>(cardGameController, this);
            DiscardPile = new FieldRegion<TCard, TCardDefinition>(cardGameController, this);
        }

        public void DrawCard(TCard card)
        {
            MoveCardFromRegionToRegion(Deck, Hand, card);
        }

        public void DrawTopCard()
        {
            //MoveCardFromRegionToRegion(Deck, Hand, card);
        }

        public void DiscardCard(TCard card)
        {
            MoveCardFromRegionToRegion(Hand, DiscardPile, card);
        }

        public void MillTopCard()
        {
            //MoveCardFromRegionToRegion(Deck, Hand, card);
        }

        public void DiscardCardFromDeck(TCard card)
        {
            MoveCardFromRegionToRegion(Deck, DiscardPile, card);
        }

        private void MoveCardFromRegionToRegion(FieldRegion<TCard, TCardDefinition> sourceRegion, FieldRegion<TCard, TCardDefinition> destinationRegion, TCard card)
        {

        }
    }
}
