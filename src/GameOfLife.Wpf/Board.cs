using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Wpf
{
    public class Board
    {
        /// <summary>
        /// A tábla mezői, 1. dimenzió sor, 2. dim oszlop
        /// </summary>
        readonly bool[,] Data;

        public Board(int width, int height)
        {
            if (width < 1 || width > 200)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height < 1 || height > 200)
                throw new ArgumentOutOfRangeException(nameof(height));


            Data = new bool[height,width];
        }


    }
}
