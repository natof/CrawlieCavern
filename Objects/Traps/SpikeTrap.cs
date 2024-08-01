namespace DefaultNamespace {
    public class SpikeTrap : DeathCollisionTrap {
        protected override DeathCause GetPlayerDeathCause() {
            return DeathCause.Spike;
        }
    }
}