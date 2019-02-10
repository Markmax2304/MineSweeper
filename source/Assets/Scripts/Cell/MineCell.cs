using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class MineCell : Cell
    {
        public override CellType GetTypeCell()
        {
            return CellType.Mine;
        }

        public override void OpenCell()
        {
            if (State == CellStates.Closed) {
                State = CellStates.Opened;
                ChangeSprite(cellSprites.explosingMineCell);
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

        public void OpenMine()
        {
            ChangeSprite(cellSprites.mineCell);
        }

        public void OpenDefeatMine()
        {
            ChangeSprite(cellSprites.defeatMineCell);
        }
    }
}
