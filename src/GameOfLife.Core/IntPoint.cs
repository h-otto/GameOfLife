﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    public struct IntPoint
    {
        public IntPoint(int row, int col)
        {
            Col = col;
            Row = row;
        }

        public readonly int Col;
        public readonly int Row;
    }
}
