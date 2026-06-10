using System;
using System.Collections.Generic;

namespace BeneathThePines
{
    [Serializable]
    public class PlayerState
    {
        public int MaxStamina = 100;
        public int Stamina = 100;

        public int MaxWarmth = 100;
        public int Warmth = 100;

        public int MaxMorale = 100;
        public int Morale = 100;

        public int MaxDaylight = 100;
        public int Daylight = 100;

        public int WaterUnits = 2;
        public int FoodUnits = 2;

        public LoadTier LoadTier = LoadTier.Balanced;

        public List<StatusEffectType> StatusEffects = new();
    }
}
