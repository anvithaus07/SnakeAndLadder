
namespace SnakeAndLadder
{
    public class EventManager
    {
        private static EventManager m_Instance;

        public static EventManager Instance()
        {
            if (m_Instance == null)
                m_Instance = new EventManager();
            return m_Instance;
        }

        public delegate void OnDiceRolledByPlayerDel();
        public event OnDiceRolledByPlayerDel OnDiceRolledByPlayer;

        public void OnDiceRolledByPlayerEvent()
        {
            OnDiceRolledByPlayer?.Invoke();
        }

        public delegate void OnPlayerMovementCompletedDel();
        public event OnPlayerMovementCompletedDel OnPlayerMovementCompleted;

        public void OnPlayerMovementCompletedEvent()
        {
            OnPlayerMovementCompleted?.Invoke();
        }
    }
}