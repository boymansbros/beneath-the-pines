using System;
using System.Collections.Generic;

namespace BeneathThePines
{
    [Serializable]
    public class RunState
    {
        public string ExpeditionId;
        public int DayIndex = 1;

        public bool IsRunComplete;
        public bool IsRunFailed;

        public WeatherType CurrentWeather = WeatherType.Clear;

        public PlayerState Player = new();
        public MapState Map = new();
        public CampRuntimeState Camp = new();

        public List<string> LogEntries = new();
    }
}
