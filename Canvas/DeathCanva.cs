namespace DefaultNamespace {
    public class DeathCanva : Canva {

        public void OnClickButtonRestart() {
            GameManager.Get().ReloadGame();
        }
    }
}