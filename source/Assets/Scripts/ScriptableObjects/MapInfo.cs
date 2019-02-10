using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    [CreateAssetMenu(fileName = "MapInfo", menuName = "ScriptObj/AddMapInfo", order = 52)]
    public class MapInfo : ScriptableObject
    {
        public int height = 10;
        public int width = 10;
        public int mineCount = 15;
        public bool useSeed = true;
        public string seed = "Game";

        public EmptyCell cell;
        public MineCell mine;
        public UIElement resetButton;
    }
}
