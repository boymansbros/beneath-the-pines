namespace BeneathThePines
{
    public static class RunFactory
    {
        public static RunState CreateNewRun()
        {
            RunState run = new RunState();

            run.ExpeditionId = "exp_fire_tower_mvp";
            run.DayIndex = 1;
            run.CurrentWeather = WeatherType.Clear;

            run.Player = new PlayerState();
            run.Map = new MapState
            {
                CurrentNodeId = "start"
            };

            run.Camp = new CampRuntimeState();

            run.LogEntries.Add("New expedition started.");

            return run;
        }
    }
}
