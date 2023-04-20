using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeAndLadder
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_ladder;

        private Vector3 mStartPoint;
        private Vector3 mEndPoint;

        private int StartPointAsNum;
        private int EndPointAsNum;


        public void InitailizeLadder(Tile startTile, Tile endTile)
        {
            mStartPoint = startTile.TileTransformPos;
            mEndPoint = endTile.TileTransformPos;

            StartPointAsNum = startTile.TileNum;
            EndPointAsNum = endTile.TileNum;

            m_ladder.SetPosition(0, mStartPoint);
            m_ladder.SetPosition(1, mEndPoint);

            startTile.IsAlreadyOccupied = true;
            endTile.IsAlreadyOccupied = true;
        }

        public Vector3 GetStartPoint()
        {
            return mStartPoint;
        }

        public Vector3 GetEndPoint()
        {
            return mEndPoint;
        }

        public int GetEndPosAsNum()
        {
            return EndPointAsNum;
        }
    }
}