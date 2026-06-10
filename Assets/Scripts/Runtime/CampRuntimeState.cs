using System;
using System.Collections.Generic;

namespace BeneathThePines
{
    [Serializable]
    public class CampRuntimeState
    {
        public bool ShelterSet;
        public bool WaterPrepared;
        public bool MealEaten;
        public bool FireLit;
        public bool GearDried;

        public List<string> CompletedActionIds = new();
    }
}
