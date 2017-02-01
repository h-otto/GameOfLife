using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    /// <summary>
    /// Egy generációból létrehoz egy újat
    /// </summary>
    public class Stepper
    {
        static readonly List<Point> Neighbours = new List<Point>
        {
            new Point(-1,-1), new Point(0,-1), new Point(1,-1), new Point(1,0),
            new Point(1,1), new Point(0,1), new Point(-1,1), new Point(-1,0),
        };

        public bool[,] GetNextGeneration(bool[,] initial)
        {
            if (initial == null)
                throw new ArgumentNullException(nameof(initial));

            int height = initial.GetLength(0);
            int width = initial.GetLength(1);

            var neighbourCounts = new int[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    for (int i = 0; i < Neighbours.Count; i++)
                    {
                        var neighbourPosition = new Point(row + Neighbours[i].Row, col + Neighbours[i].Col);
                        if (neighbourPosition.Col >= 0 && neighbourPosition.Row >= 0
                            && neighbourPosition.Col < width && neighbourPosition.Row < height)
                        {
                            if(initial[neighbourPosition.Row, neighbourPosition.Col])
                                neighbourCounts[row, col]++;
                        }
                    }
                }
            }

            //az előző generációból indulunk ki
            bool[,] nextGen = (bool[,])initial.Clone();

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (neighbourCounts[row, col] < 2 || neighbourCounts[row, col] > 3)
                        //meghal
                        nextGen[row, col] = false;

                    else if (neighbourCounts[row, col] == 3)
                        //új keletkezik
                        nextGen[row, col] = true;

                }
            }

            return nextGen;
        }
    }
}
