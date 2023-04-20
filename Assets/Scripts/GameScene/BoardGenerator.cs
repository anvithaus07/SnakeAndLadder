using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeAndLadder
{
    public class BoardGenerator : MonoBehaviour
    {

        [SerializeField] private Camera m_mainCamera;

        [Header("Board Elements")]
        [SerializeField] private Ladder m_ladder;
        [SerializeField] private Snake m_snake;
        [SerializeField] private BoardTile m_boardTile;

        [Header("Board Element Holder")]
        [SerializeField] private Transform m_boardTileHolder;
        [SerializeField] private Transform m_snakeHolder;
        [SerializeField] private Transform m_ladderHolder;

        List<Snake> Snakes = new List<Snake>();
        List<Ladder> Ladders = new List<Ladder>();
        List<Tile> mBoardTiles = new List<Tile>();

        int mCellCount = 0;

        public void GenerateBoard(int rows, int columns)
        {
            mCellCount = 0;
            mBoardTiles = new List<Tile>();
            m_mainCamera.transform.position = new Vector3((float)rows / 2 - 0.5f, (float)columns / 2 - 0.5f, -10f);

            GenerateTiles(rows, columns);
            GenerateSnakesAndLadders();


            BoardManager.Instance().Snakes = Snakes;
            BoardManager.Instance().Ladders = Ladders;
            BoardManager.Instance().BoardTiles = mBoardTiles;
        }

        void GenerateTiles(int rows, int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        InstantiateTile(j, i);
                    }
                }
                else
                {
                    for (int j = rows - 1; j >= 0; j--)
                    {
                        InstantiateTile(j, i);
                    }
                }

            }
        }
        void InstantiateTile(int xPos, int yPos)
        {
            var spawnedTile = Instantiate(m_boardTile, new Vector3(xPos, yPos), Quaternion.identity);
            mCellCount++;
            spawnedTile.InitializeTile(mCellCount, xPos, yPos);
            spawnedTile.transform.SetParent(m_boardTileHolder);
            mBoardTiles.Add(new Tile() { TileNum = mCellCount, TileXCordinate = xPos, TileYCordinate = yPos, TileTransformPos = spawnedTile.transform.position });
        }
        
        #region Snake Generation

        private void GenerateSnakePoints(List<Tile> boardTiles)
        {
            int maxXGridCross = 4;
            int endXPos = 0;
            Tile endTile;
            boardTiles.RemoveAt(0);
            boardTiles.RemoveAt(BoardManager.Instance().GetBoardSize() - 2);
            boardTiles.RemoveAll(x => x.IsAlreadyOccupied);

            while (Snakes.Count < BoardManager.Instance().SnakeCount)
            {
                List<Tile> possibleSnakeTiles = boardTiles.Where(x => x.TileYCordinate != 0).ToList();

                Tile startTile = possibleSnakeTiles[Random.Range(0, possibleSnakeTiles.Count)];

                int leftXDistance = startTile.TileXCordinate;
                int rightXDistance = BoardManager.Instance().Rows - 1 - startTile.TileXCordinate;

                bool shouldChooseRightSide = true;
                if (leftXDistance != 0)
                    shouldChooseRightSide = Random.Range(0, 2) % 2 == 0;

                if (shouldChooseRightSide)
                {
                    int gridCrossCount = Random.Range(0, rightXDistance > maxXGridCross ? maxXGridCross + 1 : rightXDistance + 1);
                    endXPos = startTile.TileXCordinate + gridCrossCount;
                }
                else
                {
                    int gridCrossCount = Random.Range(1, leftXDistance > maxXGridCross ? maxXGridCross + 1 : leftXDistance);
                    endXPos = startTile.TileXCordinate - gridCrossCount;
                }

                List<Tile> avaiableSnakeTiles = boardTiles.Where(x => x.TileXCordinate == endXPos && x.TileYCordinate > startTile.TileYCordinate).ToList();

                if (avaiableSnakeTiles.Count > 0)
                {
                    endTile = avaiableSnakeTiles[Random.Range(0, avaiableSnakeTiles.Count)];

                    if (endTile.TileXCordinate != startTile.TileXCordinate)
                    {
                        var spawnedSnake = Instantiate(m_snake, new Vector3(0, 0), Quaternion.identity);
                        spawnedSnake.InitailizeSnake(startTile, endTile);
                        spawnedSnake.transform.SetParent(m_snakeHolder);
                        Snakes.Add(spawnedSnake.GetComponent<Snake>());

                        boardTiles.Remove(startTile);
                        boardTiles.Remove(endTile);
                    }
                }
            }
        }
        #endregion Snake Generation

        #region Ladder Generation

        void GenerateSnakesAndLadders()
        {
            GenerateLadderPoints(new List<Tile>(mBoardTiles));

            GenerateSnakePoints(new List<Tile>(mBoardTiles));
        }
        private void GenerateLadderPoints(List<Tile> boardTiles)
        {
            int maxXGridCross = 4;
            int endXPos = 0;
            Tile endTile;
            boardTiles.RemoveAt(0);
            boardTiles.RemoveAt(BoardManager.Instance().GetBoardSize() - 2);
            while (Ladders.Count < BoardManager.Instance().LadderCount)
            {
                List<Tile> possibleLadderTiles = boardTiles.Where(x => x.TileYCordinate != BoardManager.Instance().Columns - 1).ToList();

                Tile startTile = possibleLadderTiles[Random.Range(0, possibleLadderTiles.Count)];

                int leftXDistance = startTile.TileXCordinate;
                int rightXDistance = BoardManager.Instance().Rows - 1 - startTile.TileXCordinate;
                int upperYDistance = BoardManager.Instance().Columns - 1 - startTile.TileYCordinate;

                bool shouldChooseRightSide = true;
                if (leftXDistance != 0)
                    shouldChooseRightSide = Random.Range(0, 2) % 2 == 0;

                if (shouldChooseRightSide)
                {
                    int gridCrossCount = Random.Range(0, rightXDistance > maxXGridCross ? maxXGridCross + 1 : rightXDistance + 1);
                    endXPos = startTile.TileXCordinate + gridCrossCount;
                }
                else
                {
                    int gridCrossCount = Random.Range(1, leftXDistance > maxXGridCross ? maxXGridCross + 1 : leftXDistance);
                    endXPos = startTile.TileXCordinate - gridCrossCount;
                }

                List<Tile> avaiableLadderTiles = boardTiles.Where(x => x.TileXCordinate == endXPos && x.TileYCordinate > startTile.TileYCordinate).ToList();

                if (avaiableLadderTiles.Count > 0)
                {
                    endTile = avaiableLadderTiles[Random.Range(0, avaiableLadderTiles.Count)];

                    if (endTile.TileXCordinate != startTile.TileXCordinate)
                    {
                        var spawnedLadder = Instantiate(m_ladder, new Vector3(0, 0), Quaternion.identity);
                        spawnedLadder.InitailizeLadder(startTile, endTile);
                        spawnedLadder.transform.SetParent(m_ladderHolder);
                        Ladders.Add(spawnedLadder.GetComponent<Ladder>());

                        boardTiles.Remove(startTile);
                        boardTiles.Remove(endTile);

                        Tile t = mBoardTiles.FirstOrDefault(x => x.TileNum == startTile.TileNum);
                        t.IsAlreadyOccupied = true;
                        int index = mBoardTiles.FindIndex(x => x.TileNum == startTile.TileNum);
                        mBoardTiles[index] = t;

                        t = mBoardTiles.FirstOrDefault(x => x.TileNum == endTile.TileNum);
                        t.IsAlreadyOccupied = true;
                        index = mBoardTiles.FindIndex(x => x.TileNum == endTile.TileNum);
                        mBoardTiles[index] = t;

                    }
                }
            }
        }
        #endregion Ladder Generation
    }
}