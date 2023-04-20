using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SnakeAndLadder
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Player Input")]
        [SerializeField] private IncrementDecrementComp m_broadGridSize;
        [SerializeField] private IncrementDecrementComp m_SnakeCount;
        [SerializeField] private IncrementDecrementComp m_LadderCount;
        [SerializeField] private IncrementDecrementComp m_playerCount;

        [Header("Start Game")]
        [SerializeField] private Button m_StartGame;


        const int kDefaultRowCount = 10;
        const int kDefaultSnakeCount = 1;
        const int kDefaultLadderCount = 1;
        const int kDefalutPlayerCount = 2;

        int mBoardRowCount = kDefaultRowCount;
        int mColumnCount = kDefaultRowCount;
        int mSnakeCount = kDefaultSnakeCount;
        int mLadderCount = kDefaultLadderCount;
        int mPlayerCount = kDefalutPlayerCount;

        #region Unity Events
        private void Start()
        {
            m_broadGridSize.Initailize("Grid Size", 10, 5, kDefaultRowCount, OnBoardGridSizeChanged, OnBoardGridSizeChanged);
            m_SnakeCount.Initailize("Snake Count", 10, 1, kDefaultSnakeCount, OnSnakeCountChanged, OnSnakeCountChanged);
            m_LadderCount.Initailize("Ladder Count", 10, 1, kDefaultLadderCount, OnLadderCountChanged, OnLadderCountChanged);
            m_playerCount.Initailize("Player Count", 4, 2, kDefalutPlayerCount, OnPlayerCountChanged, OnPlayerCountChanged);

            m_LadderCount.UpdateMaxAndMinValues(1, mBoardRowCount / 2);
            m_SnakeCount.UpdateMaxAndMinValues(1, mBoardRowCount / 2);
        }
        private void OnEnable()
        {
            m_StartGame.onClick.AddListener(OnStartGameBtnClicked);
        }
        private void OnDisable()
        {
            m_StartGame.onClick.RemoveAllListeners();
        }
        #endregion Unity Events

        #region Player Input Processing

        void OnBoardGridSizeChanged(int count)
        {
            mBoardRowCount = count;
            mColumnCount = count;
            m_LadderCount.UpdateMaxAndMinValues(1, mBoardRowCount / 2);
            m_SnakeCount.UpdateMaxAndMinValues(1, mBoardRowCount / 2);
        }


        void OnSnakeCountChanged(int count)
        {
            mSnakeCount = count;
        }
        void OnLadderCountChanged(int count)
        {
            mLadderCount = count;
        }

        void OnPlayerCountChanged(int count)
        {
            mPlayerCount = count;
        }

        #endregion Player Input Processing

        void OnStartGameBtnClicked()
        {
            BoardManager.Instance().SetBoardParametes(mBoardRowCount, mColumnCount, mSnakeCount, mLadderCount, mPlayerCount);
            SceneManager.LoadScene("GameScene");
        }
    }
}