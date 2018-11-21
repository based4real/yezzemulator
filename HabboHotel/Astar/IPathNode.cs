using System;

namespace Yezz.HabboHotel.Astar
{
    public interface IPathNode
    {
        bool IsBlocked(int x, int y, bool lastTile);
    }
}