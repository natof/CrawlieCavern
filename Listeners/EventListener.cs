namespace DefaultNamespace {
    public static class EventListener {
        public static void Init() {
            EventManager.Subscribe<PlayerDeathEvent>(PlayerDeathListener.OnPlayerDeathEvent);
        }
    }
}