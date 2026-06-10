namespace BeneathThePines
{
    public class TrailCardDefinition
    {
        public string CardId;
        public string DisplayName;

        public int DaylightCost;
        public int StaminaCost;
        public int WarmthDelta;
        public int MoraleDelta;
        public int WaterDelta;
        public int FoodDelta;

        public StatusEffectType? AppliedStatusEffect;
    }
}
