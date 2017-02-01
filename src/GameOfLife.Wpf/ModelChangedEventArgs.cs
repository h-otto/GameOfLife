using GameOfLife.Core;
using System;
using System.Collections.Generic;

namespace GameOfLife.Wpf
{
    public class ModelChangedEventArgs : EventArgs
    {
        public ModelChangedEventArgs(EModelChangeType type)
        {
            Type = type;
        }

        public readonly EModelChangeType Type;
        public readonly List<IntPoint> ChangedCells = new List<IntPoint>();
    }

    public enum EModelChangeType
    {
        Reset,
        CellList,
    }
}
