

namespace MineSweeper
{
    public enum CellStates { Closed, Opened, Flag};
    public enum CellType { Cell, Mine};

    public enum EventType { Open, SetFlag};

    public delegate void CellEventHandler(Cell cell);
    public delegate void GameEventHandler();
}
