using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace SnakeAndLadder
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] private Button m_diceButton;
        [SerializeField] private TextMeshProUGUI m_rolledNumber;
        [SerializeField] private GamePlayManager m_gamePlayManager;

        private void OnEnable()
        {
            m_diceButton.onClick.AddListener(OnDiceRolled);
            EventManager.Instance().OnPlayerMovementCompleted += OnPlayerMovementCompleted;
        }

        private void OnDisable()
        {
            m_diceButton.onClick.RemoveAllListeners();
            EventManager.Instance().OnPlayerMovementCompleted -= OnPlayerMovementCompleted;
        }

        void OnDiceRolled()
        {
            int number = Random.Range(1, 7);
            m_rolledNumber.text = number.ToString();
            EnableDisableDiceInterAction(false);
            EventManager.Instance().OnDiceRolledByPlayerEvent();
            m_gamePlayManager.MovePlayerCoin(number);

        }

        public void EnableDisableDiceInterAction(bool canPlay)
        {
            m_diceButton.interactable = canPlay;
        }

        void OnPlayerMovementCompleted()
        {
            m_rolledNumber.text = "";
            EnableDisableDiceInterAction(true);
        }
    }
}