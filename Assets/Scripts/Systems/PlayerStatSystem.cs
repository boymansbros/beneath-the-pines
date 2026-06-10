namespace BeneathThePines
{
    public class PlayerStatSystem
    {
        public void ModifyStamina(PlayerState player, int amount)
        {
            player.Stamina = Clamp(player.Stamina + amount, 0, player.MaxStamina);
        }

        public void ModifyWarmth(PlayerState player, int amount)
        {
            player.Warmth = Clamp(player.Warmth + amount, 0, player.MaxWarmth);
        }

        public void ModifyMorale(PlayerState player, int amount)
        {
            player.Morale = Clamp(player.Morale + amount, 0, player.MaxMorale);
        }

        public void ModifyDaylight(PlayerState player, int amount)
        {
            player.Daylight = Clamp(player.Daylight + amount, 0, player.MaxDaylight);
        }

        public void ApplyStatus(PlayerState player, StatusEffectType status)
        {
            if (!player.StatusEffects.Contains(status))
                player.StatusEffects.Add(status);
        }

        public void RemoveStatus(PlayerState player, StatusEffectType status)
        {
            player.StatusEffects.Remove(status);
        }

        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
