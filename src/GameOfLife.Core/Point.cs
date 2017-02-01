using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    struct Point
    {
        public Point(int row, int col)
        {
            Col = col;
            Row = row;
        }

        public readonly int Col;
        public readonly int Row;
    }
}
