namespace BeneathThePines
{
    public class TrailCardResolver
    {
        private readonly PlayerStatSystem _playerStats;

        public TrailCardResolver(PlayerStatSystem playerStats)
        {
            _playerStats = playerStats;
        }

        public void Resolve(TrailCardDefinition card, RunState run)
        {
            _playerStats.ModifyDaylight(run.Player, -card.DaylightCost);
            _playerStats.ModifyStamina(run.Player, -card.StaminaCost);
            _playerStats.ModifyWarmth(run.Player, card.WarmthDelta);
            _playerStats.ModifyMorale(run.Player, card.MoraleDelta);

            run.Player.WaterUnits += card.WaterDelta;
            run.Player.FoodUnits += card.FoodDelta;

            if (card.AppliedStatusEffect.HasValue)
                _playerStats.ApplyStatus(run.Player, card.AppliedStatusEffect.Value);

            run.LogEntries.Add($"Resolved card: {card.DisplayName}");
        }
    }
}
