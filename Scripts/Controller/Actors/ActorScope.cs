using CcgCore.Controller.Cards;
using CcgCore.Controller.Events;
using CcgCore.Model;
using CcgCore.Model.Effects;
using CcgCore.Model.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace CcgCore.Controller.Actors
{
    public class ActorScope : ParameterScope
    {
        public Actor Actor { get; private set; }

        public ActorScope(ParameterScope parentScope) 
            : base(ParameterScopeLevel.Actor, parentScope)
        {
        }

        public void Initialise(Actor actor, ActorTemplate playerTemplate)
        {
            Actor = actor;
            for (int i = 0; i < playerTemplate.Regions.Count; i++)
            {
                var region = new FieldRegion(playerTemplate.Regions[i].TemplateScope, this);

                foreach (var cd in playerTemplate.Regions[i].Cards)
                    region.AddCard(cd);
            }
        }

        protected override List<(ParameterScope scope, TriggeredEffect effect)> GetTriggeredEffectsForEvent(CardGameEvent cardGameEvent)
        {
            if (Actor == null || !Actor.ActorTemplate)
                return base.GetTriggeredEffectsForEvent(cardGameEvent);
            return Actor.ActorTemplate.TriggeredEffects
                .Where(te => te.CheckConditions(cardGameEvent, this))
                .Select(te => (this as ParameterScope, te))
                .ToList();
        }
    }
}
