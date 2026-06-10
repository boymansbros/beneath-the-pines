namespace BeneathThePines
{
    public class NightResolutionSystem
    {
        private readonly PlayerStatSystem _playerStats;

        public NightResolutionSystem(PlayerStatSystem playerStats)
        {
            _playerStats = playerStats;
        }

        public void ResolveNight(RunState run)
        {
            int staminaRecovery = 25;
            int warmthRecovery = 10;
            int moraleRecovery = 10;

            if (run.Camp.ShelterSet)
                warmthRecovery += 15;
            else
                warmthRecovery -= 25;

            if (run.Camp.MealEaten)
                staminaRecovery += 15;

            if (run.Camp.FireLit)
                warmthRecovery += 10;

            if (run.Camp.WaterPrepared)
                moraleRecovery += 5;

            if (run.CurrentWeather == WeatherType.Rain)
                warmthRecovery -= 10;

            _playerStats.ModifyStamina(run.Player, staminaRecovery);
            _playerStats.ModifyWarmth(run.Player, warmthRecovery);
            _playerStats.ModifyMorale(run.Player, moraleRecovery);

            run.Player.Daylight = run.Player.MaxDaylight;
            run.DayIndex++;

            run.Camp = new CampRuntimeState();

            run.LogEntries.Add("Night resolved. A new day begins.");
        }
    }
}
