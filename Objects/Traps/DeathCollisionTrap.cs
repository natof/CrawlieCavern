using DefaultNamespace.objects.traps;
using UnityEngine;

namespace DefaultNamespace {
    public abstract class DeathCollisionTrap : TrapObject {

        private void OnTriggerEnter2D(Collider2D other) {
            if (Player.TryGetPlayer(other.gameObject, out Player player)) {
                return;
            }
            player.Kill(GetPlayerDeathCause());
        }

        /// <summary>
        ///     Abstract method that returns the cause of a player's death.
        /// </summary>
        /// <returns>A <see cref="DeathCause" /> representing the death cause.</returns>
        protected abstract DeathCause GetPlayerDeathCause();
    }
}