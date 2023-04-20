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
        [SerializeField] private SpriteRenderer m_tileSprite;
        [SerializeField] private TextMeshPro m_tileValue;



        public void InitializeTile(int tileNum)
        {
            m_tileSprite.color = tileNum % 2 != 0 ? m_oddColor : m_evenColor;
            m_tileValue.text = $"{tileNum}";
            gameObject.name = $"Tile_{tileNum}";
        }
    }
}