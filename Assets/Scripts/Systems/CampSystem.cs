namespace BeneathThePines
{
    public class CampSystem
    {
        public bool CanPerformAction(CampActionDefinition action, RunState run)
        {
            if (run.Player.Daylight < action.DaylightCost) return false;
            if (run.Player.WaterUnits < action.WaterCost) return false;
            if (run.Player.FoodUnits < action.FoodCost) return false;

            return true;
        }

        public void PerformAction(CampActionDefinition action, RunState run)
        {
            if (!CanPerformAction(action, run))
            {
                run.LogEntries.Add($"Could not perform: {action.DisplayName}");
                return;
            }

            run.Player.Daylight -= action.DaylightCost;
            run.Player.WaterUnits -= action.WaterCost;
            run.Player.FoodUnits -= action.FoodCost;

            if (!run.Camp.CompletedActionIds.Contains(action.ActionId))
                run.Camp.CompletedActionIds.Add(action.ActionId);

            if (action.ActionId == "set_shelter") run.Camp.ShelterSet = true;
            if (action.ActionId == "filter_water") run.Camp.WaterPrepared = true;
            if (action.ActionId == "eat_meal") run.Camp.MealEaten = true;
            if (action.ActionId == "light_fire") run.Camp.FireLit = true;
            if (action.ActionId == "dry_gear") run.Camp.GearDried = true;

            run.LogEntries.Add($"Camp action completed: {action.DisplayName}");
        }
    }
}
