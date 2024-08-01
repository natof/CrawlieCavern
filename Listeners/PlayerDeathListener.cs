using System.Collections;
using UnityEngine;

namespace DefaultNamespace {
    public static class PlayerDeathListener {
        public static void OnPlayerDeathEvent(PlayerDeathEvent @event) {
            Player player = @event.Player;
            player.PlayerDataManager.PlayerData.DeathCount++;
            CanvaManager.Get().HudCanva.UpdateDeathCounter();

            player.SetFreeze();
            player.Animator.SetInteger("death", (int)@event.DeathCause);

            GameManager.Get().GameIsInProgress = false;
            player.StartCoroutine(SendDeathScreen());
        }

        private static IEnumerator SendDeathScreen() {
            yield return new WaitForSeconds(1);
            CanvaManager.Get().DeathCanva.SetActive();
        }
    }
}