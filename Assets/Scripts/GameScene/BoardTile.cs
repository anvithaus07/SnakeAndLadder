using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SnakeAndLadder
{
    public struct Tile
    {
        public int TileNum;
        public int TileXCordinate;
        public int TileYCordinate;
        public Vector3 TileTransformPos;
        public bool IsAlreadyOccupied;
    }

    public class BoardTile : MonoBehaviour
    {
        [SerializeField] private Color m_oddColor;
        [SerializeField] private Color m_evenColor;
        [SerializeField] private Color m_startEndColor;
        [SerializeField] private SpriteRenderer m_tileSprite;
        [SerializeField] private TextMeshPro m_tileValue;



        public void InitializeTile(int tileNum)
        {
            SetTileColor(tileNum);
            SetTileText(tileNum);
            gameObject.name = $"Tile_{tileNum}";
        }

        void SetTileText(int tileNum)
        {
            if(tileNum==1)
                m_tileValue.text = "START";
            else if(tileNum == BoardManager.Instance().GetBoardSize())
                m_tileValue.text = "END";
            else
                m_tileValue.text = $"{tileNum}";
        }

        void SetTileColor(int tileNum)
        {
            if (tileNum == 1 || tileNum == BoardManager.Instance().GetBoardSize())
                m_tileSprite.color = m_startEndColor;
            else
                m_tileSprite.color = tileNum % 2 != 0 ? m_oddColor : m_evenColor;
        }
    }
}