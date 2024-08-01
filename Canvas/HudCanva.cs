using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace {
    public class HudCanva : Canva {

        public Text deathCounter;
        public Sprite defaultDeathImage;
        public Sprite easterEggDeathImage;
        public Image deathImage;

        public void UpdateDeathCounter() {
            int amount = Player.GetPlayer().PlayerDataManager.PlayerData.DeathCount;
            deathCounter.text = amount.ToString();
            deathImage.sprite = amount == 69 ? easterEggDeathImage : defaultDeathImage;
        }
    }
}