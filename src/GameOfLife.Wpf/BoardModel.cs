using GameOfLife.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Wpf
{
    public class BoardModel
    {
        public readonly int RowCount;
        public readonly int ColCount;
        readonly Stepper Stepper;

        /// <summary>
        /// A tábla mezői, 1. dimenzió sor, 2. dim oszlop
        /// </summary>
        readonly bool[,] Data;

        public BoardModel(int rowCount, int colCount, Stepper stepper)
        {
            if (colCount < 1 || colCount > 200)
                throw new ArgumentOutOfRangeException(nameof(colCount));
            if (rowCount < 1 || rowCount > 200)
                throw new ArgumentOutOfRangeException(nameof(rowCount));
            if (stepper == null)
                throw new ArgumentNullException(nameof(stepper));


            RowCount = rowCount;
            ColCount = colCount;
            Stepper = stepper;

            Data = new bool[rowCount, colCount];
        }

        public void Step()
        {
            var newGen = Stepper.GetNextGeneration(Data);
            Debug.Assert(newGen.GetLength(0) == Data.GetLength(0));
            Debug.Assert(newGen.GetLength(1) == Data.GetLength(1));

            bool changed = false;

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    if (Data[row, col] != newGen[row, col])
                    {
                        changed = true;
                        Data[row, col] = newGen[row, col];
                    }
                }
            }

            if (changed)
                ModelChanged?.Invoke();
        }

        public bool this[int row, int col]
        {
            get
            {
                return Data[row, col];
            }
            set
            {
                if (value != Data[row, col])
                {
                    Data[row, col] = value;
                    ModelChanged?.Invoke();
                }
            }
        }

        //TODO azt átadni, hogy melyik cellák változtak
        public event Action ModelChanged;

        public void Clear()
        {
            bool changed = false;
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    if (Data[row, col] == true)
                    {
                        changed = true;
                        Data[row, col] = false;
                    }
                }
            }

            if (changed)
                ModelChanged?.Invoke();

        }
    }
}
