using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MineSweeper
{
    public class EmptyCell : Cell
    {
        public void InitCell (int x, int y, int count)
        {
            base.InitCell(x, y);
            MineAround = count;
        }

        public override CellType GetTypeCell()
        {
            return CellType.Cell;
        }

        public override void OpenCell()
        {
            if (State == CellStates.Closed) {
                State = CellStates.Opened;
                ChangeSprite(cellSprites.openNumberCells[MineAround]);
            }
        }

        public override void SetFlagCell()
        {
            if (State == CellStates.Closed) {
                State = CellStates.Flag;
                ChangeSprite(cellSprites.flagCell);
            }
            else if (State == CellStates.Flag) {
                State = CellStates.Closed;
                ChangeSprite(cellSprites.closedCell);
            }
        }
    }
}
