using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeAndLadder
{
    public class BoardManager
    {
        private static BoardManager m_instance;

        public static BoardManager Instance()
        {
            if (m_instance == null)
            {
                m_instance = new BoardManager();
            }

            return m_instance;
        }

        public void SetBoardParametes(int rows, int col, int snakes, int ladder, int playerCount)
        {
            Rows = rows;
            Columns = col;
            SnakeCount = snakes;
            LadderCount = ladder;
            PlayerCount = playerCount;
        }

        public Vector3 GetBoardStartPos()
        {
            return BoardTiles.FirstOrDefault(x => x.TileNum == 0).TileTransformPos;
        }

        public Vector3 GetBoardTilePosFor(int PosNumber)
        {
            return BoardTiles.FirstOrDefault(x => x.TileNum == PosNumber).TileTransformPos;
        }

        public List<Snake> Snakes { get; set; }
        public List<Ladder> Ladders { get; set; }
        public List<Tile> BoardTiles { get; set; }

        #region Board Parameters
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int SnakeCount { get; set; }
        public int LadderCount { get; set; }
        public int PlayerCount { get; set; }
        #endregion

        public int GetBoardSize()
        {
            return Rows * Columns;
        }
    }
}