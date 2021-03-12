namespace CcgCore.Model.Effects
{
    public enum CardEffectTrigger
    {
        CardActivation = 0,
        CardActivationFailure = 1,
        CardAddedToRegion = 7,
        TurnStart = 10,
        TurnEnd = 11,
        Destroy = 20,
        Discard = 21
    }
}
