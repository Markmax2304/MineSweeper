using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper
{
    public class UIController : MonoBehaviour
    {
        UIElement resetButton;

        public void InitButton(MapInfo info, GameEventHandler handler)
        {
            Vector2 position = new Vector2(0, info.height / 2f + 1);
            if (resetButton == null) {
                resetButton = Instantiate(info.resetButton, position, Quaternion.identity);
                SubscribeOnResetButton(handler);
            }
            else {
                resetButton.transform.position = position;
            }
        }

        public void SubscribeOnResetButton(GameEventHandler handler)
        {
            resetButton.OnResetingGame += handler;
        }

        public void VizualeEndingOnButton(bool stateGame)
        {
            resetButton.ChangeEndingSprite(stateGame);
        }
    }
}
