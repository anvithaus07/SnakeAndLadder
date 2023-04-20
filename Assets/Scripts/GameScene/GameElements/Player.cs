using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeAndLadder
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Sprite> m_playerIcons;
        [SerializeField] private SpriteRenderer m_playerSprite;

        [HideInInspector] public int PlayerId;
        private Vector3 PlayerPos;
        private int PositionAsNum;

        public void SetPlayerDetails(int Id, Vector3 pos)
        {
            PlayerId = Id;
            PlayerPos = pos;
            PositionAsNum = 0;
            m_playerSprite.sprite = m_playerIcons[Id];
        }

        Vector3 CalculateNewPosition(Vector3 newPosition, ref int newPosNum)
        {
            Vector3 previousPosition;

            do
            {
                previousPosition = newPosition;
                foreach (Snake snake in BoardManager.Instance().Snakes)
                {
                    if (snake.GetHeadPoint() == newPosition)
                    {
                        newPosition = snake.GetTailPoint();
                        newPosNum = snake.GetTailPosAsNum();
                    }
                }

                foreach (Ladder ladder in BoardManager.Instance().Ladders)
                {
                    if (ladder.GetStartPoint() == newPosition)
                    {
                        newPosition = ladder.GetEndPoint(); // Whenever a piece ends up at a position with the start of the ladder, the piece should go up to the position of the end of that ladder.
                        newPosNum = ladder.GetEndPosAsNum();
                    }
                }
            }
            while (newPosition != previousPosition);

            return newPosition;
        }

        public void MovePlayerOnDiceRoll(int moveByDiceCount)
        {
            int oldPosition = PositionAsNum;
            int newPosition = oldPosition + moveByDiceCount;

            int boardSize = BoardManager.Instance().GetBoardSize();


            if (newPosition > boardSize)
            {
                PositionAsNum = oldPosition;
                EventManager.Instance().OnPlayerMovementCompletedEvent();
            }
            else
            {
                StartCoroutine(MovePlayerStepByStep(oldPosition, newPosition));
            }
        }

        IEnumerator MovePlayerStepByStep(int oldPos, int newPos)
        {
            for (int i = oldPos + 1; i <= newPos; i++)
            {
                transform.position = BoardManager.Instance().GetBoardTilePosFor(i);
                yield return new WaitForSeconds(0.5f);
            }
            PlayerPos = CalculateNewPosition(BoardManager.Instance().GetBoardTilePosFor(newPos), ref newPos);

            if (transform.position != PlayerPos)
                transform.position = PlayerPos;

            PositionAsNum = newPos;

            EventManager.Instance().OnPlayerMovementCompletedEvent();
        }
        public bool CheckIfPlayerHasWon()
        {
            int winningPosition = BoardManager.Instance().GetBoardSize();
            return PositionAsNum == winningPosition;
        }
    }
}