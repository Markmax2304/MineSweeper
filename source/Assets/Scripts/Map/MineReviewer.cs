using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class MineReviewer
    {
        public event GameEventHandler OnWinGame;

        List<Cell> cellList;
        int countMine;
        int currentFlagMines = 0;

        public MineReviewer(int count, List<Cell> cells)
        {
            countMine = count;
            cellList = new List<Cell>();
            for (int i = 0; i < cells.Count; i++) {
                cells[i].SubscribeOnSettingFlagEvent(SetFlagInCell);
                cellList.Add(cells[i]);
            }
        }

        public void DisableCellSettingFlag()
        {
            for (int i = 0; i < cellList.Count; i++) {
                cellList[i].UnSubscribeOnSettingFlagEvent(SetFlagInCell);
            }
        }

        void SetFlagInCell(Cell cell)
        {
            if (cell.State == CellStates.Opened)
                return;
            else if(cell.State == CellStates.Closed && currentFlagMines < countMine) {
                cell.SetFlagCell();
                currentFlagMines++;
            }
            else if (cell.State == CellStates.Flag && currentFlagMines > 0) {
                cell.SetFlagCell();
                currentFlagMines--;
            }

            if (VerifyFlagCells()) {
                OnWinGame();
            }
        }

        bool VerifyFlagCells()
        {
            for(int i = 0; i < cellList.Count; i++) {
                if(cellList[i].GetTypeCell() == CellType.Mine) {
                    if (cellList[i].State != CellStates.Flag)
                        return false;
                }
            }
            return true;
        }
    }
}
