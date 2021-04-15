using CcgCore.Controller.Cards;
using CcgCore.Model;
using CcgCore.Model.Parameters;

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
    }
}
