using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MineSweeper
{
    public class UIElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event GameEventHandler OnResetingGame = delegate () { };

        public Sprite defaultSprite;
        public Sprite mousePressSprite;

        public Sprite winSprite;
        public Sprite loseSprite;

        SpriteRenderer spriteRend;

        void Start()
        {
            spriteRend = GetComponent<SpriteRenderer>();
        }

        public void OnPointerDown(PointerEventData data)
        {
            ChangeSprite(mousePressSprite);
        }

        public void OnPointerUp(PointerEventData data)
        {
            ChangeSprite(defaultSprite);
            OnResetingGame();
        }

        public void ChangeEndingSprite(bool stateGame)
        {
            if (stateGame)
                ChangeSprite(winSprite);
            else
                ChangeSprite(loseSprite);
        }

        void ChangeSprite(Sprite sprite)
        {
            spriteRend.sprite = sprite;
        }
    }
}
