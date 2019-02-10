using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class CellOpener
    {
        public event GameEventHandler OnLoseGame;

        Cell[,] cellMatrix;

        public CellOpener(int height, int width, List<Cell> cells)
        {
            cellMatrix = new Cell[height, width];
            for (int i = 0; i < cells.Count; i++) {
                Vector2Int pos = cells[i].Position;
                cellMatrix[pos.x, pos.y] = cells[i];
                cellMatrix[pos.x, pos.y].SubscribeOnOpeningEvent(OpenAllEmptyCellsAround);
            }
        }

        public void DisableCellOpeningEvents()
        {
            for (int i = 0; i < cellMatrix.GetLength(0); i++) {
                for (int j = 0; j < cellMatrix.GetLength(1); j++) {
                    if (cellMatrix[i, j] != null)
                        cellMatrix[i, j].UnSubscribeOnOpeningEvent(OpenAllEmptyCellsAround);
                }
            }
        }

        void OpenAllEmptyCellsAround(Cell cell)
        {
            int startX = cell.Position.x;
            int startY = cell.Position.y;

            cell.OpenCell();
            if (cell.GetTypeCell() == CellType.Mine) {
                OnLoseGame();
                return;
            }
            if(cell.MineAround != 0 || cell.State == CellStates.Flag)
                return;

            RemoveCell(startX, startY);

            for (int i = startX - 1; i <= startX + 1; i++) {
                for (int j = startY - 1; j <= startY + 1; j++) {
                    if (i >= 0 && i < cellMatrix.GetLength(0) && j >= 0 && j < cellMatrix.GetLength(1)) {
                        if (i != startX || j != startY) {
                            if (cellMatrix[i, j] != null)
                                OpenAllEmptyCellsAround(cellMatrix[i, j]);
                        }
                    }
                }
            }
        }

        void RemoveCell(int x, int y)
        {
            cellMatrix[x, y].UnSubscribeOnOpeningEvent(OpenAllEmptyCellsAround);
            cellMatrix[x, y] = null;
        }
    }
}
