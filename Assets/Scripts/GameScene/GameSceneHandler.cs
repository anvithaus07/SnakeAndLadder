using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace SnakeAndLadder
{
    public class GameSceneHandler : MonoBehaviour
    {
        [SerializeField] private Button m_BackBtn;
        [SerializeField] private BoardGenerator m_boardGenerator;
        [SerializeField] private GamePlayManager m_gamePlayManager;


        private void Start()
        {
            m_boardGenerator.GenerateBoard(BoardManager.Instance().Rows, BoardManager.Instance().Columns);
            m_gamePlayManager.InitializePlayers();
        }
        private void OnEnable()
        {
            m_BackBtn.onClick.AddListener(OnBackBtnClicked);
        }

        private void OnDisable()
        {
            m_BackBtn.onClick.RemoveAllListeners();
        }

        void OnBackBtnClicked()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}