using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeAndLadder
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_snake;

        private Vector3 mTailPoint;
        private Vector3 mHeadPoint;

        private int HeadPointNum;
        private int TailPointNum;


        public void InitailizeSnake(Tile tailTile, Tile headTile)
        {
            mHeadPoint = headTile.TileTransformPos;
            mTailPoint = tailTile.TileTransformPos;

            HeadPointNum = headTile.TileNum;
            TailPointNum = tailTile.TileNum;

            m_snake.SetPosition(0, mHeadPoint);
            m_snake.SetPosition(1, mTailPoint);
        }
        public Vector3 GetTailPoint()
        {
            return mTailPoint;
        }

        public Vector3 GetHeadPoint()
        {
            return mHeadPoint;
        }

        public int GetTailPosAsNum()
        {
            return TailPointNum;
        }

    }
}