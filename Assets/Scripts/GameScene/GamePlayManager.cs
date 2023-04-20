using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace SnakeAndLadder
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_playerObj;
        [SerializeField] private GameObject m_playerHolder;
        [SerializeField] private TextMeshProUGUI m_playerTurnInfo;

        [Header("Win Info")]
        [SerializeField] private TextMeshProUGUI m_winnerInfo;
        [SerializeField] private GameObject m_winnerInfoHolder;
        [SerializeField] private Canvas m_gameUICanvas;

        [Header("Game Over Info")]
        [SerializeField] private GameObject m_gameOverInfoHolder;
        [SerializeField] private Button m_replayBtn;

        float xOffsetForPlayers = 0.6f;
        List<Player> GamePlayers = new List<Player>();

        private void OnEnable()
        {
            EventManager.Instance().OnPlayerMovementCompleted += OnPlayerMovementCompleted;
            m_replayBtn.onClick.AddListener(OnReplayBtnClicked);
        }

        private void OnDisable()
        {
            EventManager.Instance().OnPlayerMovementCompleted -= OnPlayerMovementCompleted;
            m_replayBtn.onClick.RemoveAllListeners();

        }

        public void InitializePlayers()
        {
            Vector3 pos = BoardManager.Instance().GetBoardStartPos();

            for (int i = 0; i < BoardManager.Instance().PlayerCount; i++)
            {
                var player = Instantiate(m_playerObj, m_playerHolder.transform);
                player.transform.localPosition = new Vector3(xOffsetForPlayers * i, 0, 0);
                player.name = $"Player_{i}";
                Player ply = player.GetComponent<Player>();
                ply.SetPlayerDetails(i, pos);
                GamePlayers.Add(ply);
            }
        }
        private bool IsGameComplete()
        {
            return GamePlayers.Count <= 1;
        }

        public void MovePlayerCoin(int totalDiceValue)
        {
            if (!IsGameComplete())
            {
                Player currentPlayer = GamePlayers.FirstOrDefault(x => x.PlayerId == mCurrentPlayerId);
                if (currentPlayer != null)
                {
                    currentPlayer.MovePlayerOnDiceRoll(totalDiceValue);
                }
            }
        }

        void OnPlayerMovementCompleted()
        {
            Player currentPlayer = GamePlayers.FirstOrDefault(x => x.PlayerId == mCurrentPlayerId);

            if (currentPlayer.CheckIfPlayerHasWon())
            {
                StartCoroutine(ShowPlayerWinInfo(currentPlayer.PlayerId));
                GamePlayers.Remove(currentPlayer);
            }
            UpdateToNextPlayer();
        }

        #region Player Turn Handler

        int mCurrentPlayerId = 0;

        public int GetPlayablePlayerId()
        {
            return mCurrentPlayerId;
        }

        public void UpdateToNextPlayer()
        {
            if (mCurrentPlayerId < BoardManager.Instance().PlayerCount - 1)
                mCurrentPlayerId++;
            else
                mCurrentPlayerId = 0;

            m_playerTurnInfo.text = $"{GetPlayerColorFor(mCurrentPlayerId)} Turn";
        }
        #endregion Player Turn Handler

        string GetPlayerColorFor(int id)
        {
            switch (id)
            {
                case 0:
                    return "Player Red";
                case 1:
                    return "Player Yellow";
                case 2:
                    return "Player Green";
                case 3:
                    return "Player Blue";
                default:
                    return string.Empty;
            }
        }

        void OnReplayBtnClicked()
        {
            SceneManager.LoadScene("MenuScene");
        }

        IEnumerator ShowPlayerWinInfo(int playerId)
        {
            m_gameUICanvas.sortingOrder = 1;
            m_winnerInfo.text = string.Format("{0} WINS", GetPlayerColorFor(playerId)).ToUpper();
            m_winnerInfoHolder.SetActive(true);

            yield return new WaitForSeconds(3.0f);
            m_winnerInfoHolder.SetActive(false);
            m_gameUICanvas.sortingOrder = 0;

            ShowGameOverInfo();
        }

        void ShowGameOverInfo()
        {
            if (IsGameComplete())
            {
                m_gameUICanvas.sortingOrder = 1;
                m_gameOverInfoHolder.SetActive(true);
            }
        }
    }
}