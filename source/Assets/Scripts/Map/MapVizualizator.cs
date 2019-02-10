using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class MapVizualizator : MonoBehaviour
    {
        MapRandomizator mapRand;
        EmptyCell cellObj;
        MineCell mineObj;

        List<MineCell> mines = new List<MineCell>();
        List<Cell> cells = new List<Cell>();
        public int Height { private get; set; }
        public int Width { private get; set; }

        public void Init(MapInfo info)
        {
            Height = info.height;
            Width = info.width;
            cellObj = info.cell;
            mineObj = info.mine;

            mapRand = new MapRandomizator(Height, Width, info.mineCount);
            if(info.useSeed)
                Random.InitState(info.seed.GetHashCode());

            DeleteOldCells();
            mines.Clear();
            cells.Clear();
        }

        void DeleteOldCells()
        {
            for(int i = 0; i < cells.Count; i++) {
                Destroy(cells[i].gameObject);
            }
        }

        public void CreateMap()
        {
            int[,] map = mapRand.GetRandomMap();
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    if(map[i,j] == 0) {
                        EmptyCell cell = CreateCell(i, j, CalculateMineAroundCell(map, i, j));
                        cells.Add(cell);
                    }
                    else {
                        MineCell mine = CreateMine(i, j);
                        cells.Add(mine);
                        mines.Add(mine);
                    }
                }
            }
        }

        public List<Cell> GetCells()
        {
            return cells;
        }

        public List<MineCell> GetMines()
        {
            return mines;
        }

        EmptyCell CreateCell(int x, int y, int count)
        {
            EmptyCell newCell = Instantiate(cellObj, new Vector2(Width / 2f - .5f - y, -Height / 2f + .5f + x), Quaternion.identity, transform).GetComponent<EmptyCell>();
            newCell.InitCell(x, y, count);
            return newCell;
        }

        MineCell CreateMine(int x, int y)
        {
            MineCell newMine = Instantiate(mineObj, new Vector2(Width / 2f - .5f - y, -Height / 2f + .5f + x), Quaternion.identity, transform).GetComponent<MineCell>();
            newMine.InitCell(x, y);
            return newMine;
        }

        int CalculateMineAroundCell(int[,] map, int x, int y)
        {
            int count = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < map.GetLength(0) && j >= 0 && j < map.GetLength(1))
                    {
                        if (i != x || j != y)
                        {
                            if (map[i, j] == -1)
                                count++;
                        }
                    }
                }
            }
            return count;
        }
    }

    public class MapRandomizator
    {
        int height;
        int width;
        int countMine;

        public MapRandomizator(int h, int w, int count)
        {
            height = h;
            width = w;
            countMine = count;
        }

        public int[,] GetRandomMap()
        {
            int[,] map = new int[height, width];

            for (int i = 0; i < countMine;) {
                int x = Random.Range(0, height);
                int y = Random.Range(0, width);
                if (map[x, y] == 0) {
                    map[x, y] = -1;
                    i++;
                }
            }

            return map;
        }
    }
}
