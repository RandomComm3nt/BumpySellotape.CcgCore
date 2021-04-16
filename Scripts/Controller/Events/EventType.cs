namespace CcgCore.Controller.Events
{
    public enum EventType
    {
        CardActivationAttempt = 0,
        CardActivationSuccess = 1,
        CardActivationFailure = 2,
        CardRemovedFromRegion = 10,
        CardAddedToRegion = 11,
        TurnStart = 20,
        TurnEnd = 21,
        CountersAddedToCard = 30,
        CountersRemovedFromCard = 31,
        StatChanged = 40,
    }
}
