using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MineSweeper
{
    public abstract class Cell : MonoBehaviour, IPointerDownHandler
    {
        event CellEventHandler OnOpeningCell = delegate(Cell cell) { };
        event CellEventHandler OnSettingFlagCell = delegate (Cell cell) { };

        [SerializeField] protected SpriteStorage cellSprites;
        protected SpriteRenderer spriteRnd;

        public Vector2Int Position { get; protected set; }
        public CellStates State { get; protected set; }
        public int MineAround { get; protected set; }

        void Start()
        {
            spriteRnd = GetComponent<SpriteRenderer>();
        }

        public void InitCell(int x, int y)
        {
            Position = new Vector2Int(x, y);
            State = CellStates.Closed;
            MineAround = -1;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0)) {
                OnOpeningCell(this);
            }
            else if (Input.GetMouseButton(1)) {
                OnSettingFlagCell(this);
            }
        }

        protected void ChangeSprite(Sprite sprite)
        {
            spriteRnd.sprite = sprite;
        }

        public abstract CellType GetTypeCell();
        public abstract void OpenCell();
        public abstract void SetFlagCell();

        public void SubscribeOnOpeningEvent(CellEventHandler action)
        {
            OnOpeningCell += action;
        }

        public void UnSubscribeOnOpeningEvent(CellEventHandler action)
        {
            OnOpeningCell -= action;
        }

        public void SubscribeOnSettingFlagEvent(CellEventHandler action)
        {
            OnSettingFlagCell += action;
        }

        public void UnSubscribeOnSettingFlagEvent(CellEventHandler action)
        {
            OnSettingFlagCell -= action;
        }
    }
}
