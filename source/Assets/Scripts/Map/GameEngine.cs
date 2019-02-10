using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class GameEngine : MonoBehaviour
    {
        [SerializeField] MapInfo mapInfo;
        MapVizualizator mapVizual;
        UIController uiContr;
        CellOpener cellOpen;
        MineReviewer mineRev;

        void Start()
        {
            mapVizual = GetComponent<MapVizualizator>();
            uiContr = GetComponent<UIController>();
            
            ResetMap();
        }

        public void ResetMap()
        {
            mapVizual.Init(mapInfo);
            mapVizual.CreateMap();

            cellOpen = new CellOpener(mapInfo.height, mapInfo.width, mapVizual.GetCells());
            mineRev = new MineReviewer(mapInfo.mineCount, mapVizual.GetCells());

            SubscribingOnEvent();

            uiContr.InitButton(mapInfo, ResetMap);
        }

        void SubscribingOnEvent()
        {
            cellOpen.OnLoseGame += cellOpen.DisableCellOpeningEvents;
            cellOpen.OnLoseGame += mineRev.DisableCellSettingFlag;
            cellOpen.OnLoseGame += OpenDisabledMines;
            cellOpen.OnLoseGame += FinishLoseGame;

            mineRev.OnWinGame += cellOpen.DisableCellOpeningEvents;
            mineRev.OnWinGame += mineRev.DisableCellSettingFlag;
            mineRev.OnWinGame += OpenDisabledMines;
            mineRev.OnWinGame += FinishWinGame;
        }

        void OpenDisabledMines()
        {
            List<MineCell> mines = mapVizual.GetMines();
            for(int i = 0; i < mines.Count; i++) {
                if (mines[i].State == CellStates.Flag)
                    mines[i].OpenDefeatMine();
                else if (mines[i].State == CellStates.Closed)
                    mines[i].OpenMine();
            }
        }

        void FinishWinGame()
        {
            uiContr.VizualeEndingOnButton(true);
        }

        void FinishLoseGame()
        {
            uiContr.VizualeEndingOnButton(false);
        }
    }
}

