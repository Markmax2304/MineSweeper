using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    [CreateAssetMenu(fileName = "SpriteStorage", menuName = "ScriptObj/AddSpriteStorage", order = 52)]
    public class SpriteStorage : ScriptableObject
    {
        public Sprite closedCell;
        public Sprite[] openNumberCells = new Sprite[9];
        public Sprite mineCell;
        public Sprite defeatMineCell;
        public Sprite explosingMineCell;
        public Sprite flagCell;
    }
}
