using CubeMazeModel.Model;
using System;

namespace CubeMazeModel.Printers
{
    internal class CellPrinter : BasePrinter
    {
        public CellPrinter(int left, int top, Cell cell) : base(left, top)
        {
            Cell = cell;
        }

        public Cell Cell { get; }

        public override void Print() 
        {
            Console.SetCursorPosition(Left, Top);

            char ch = Cell.Visited ? 
                '+' : 
                ' ';

            Console.Write(ch);
        }
    }
}
