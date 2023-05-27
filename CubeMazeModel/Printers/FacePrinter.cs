using CubeMazeModel.Enums;
using CubeMazeModel.Model;
using System;
using System.Collections.Generic;

namespace CubeMazeModel.Printers
{
    internal class FacePrinter : BasePrinter
    {
        private readonly List<CellPrinter> _cellPrinters = new List<CellPrinter>();

        public FacePrinter(int left, int top, Face face) : base(left, top)
        {
            Face = face;
            InitializeCellPrinters();
        }

        public Face Face { get; }

        public override void Print()
        {
            _cellPrinters.ForEach(cellPrinter => cellPrinter.Print());
            PrintBorders();
        }

        private void PrintBorders()
        {
            var startX = Left-1;
            var endX = Left+Face.Size;

            var startY = Top -1;
            var endY = Top + Face.Size;

            var lenght = Face.Size + 2;

            PrintHorizontalBorder(startX, startY, lenght);
            PrintHorizontalBorder(startX, endY, lenght);

            PrintVerticalBorder(startX, startY, lenght);
            PrintVerticalBorder(endX, startY, lenght);
        }

        private void PrintHorizontalBorder(int left, int top, int lenght)
        {
            for (int i = left; i < left + lenght; i++)
            {
                PrintBorder(i, top);
            }
        }

        private void PrintVerticalBorder(int left, int top, int lenght)
        {
            for (int i = top; i < top + lenght; i++)
            {
                PrintBorder(left, i);
            }
        }

        private void PrintBorder(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write('#');
        }

        private void InitializeCellPrinters()
        {
            _cellPrinters.Clear();

            var centerCell = Face.Center;
            var edgeSteps = Face.Size / 2;

            var topCenterCell = Face.MoveFewSteps(edgeSteps,centerCell,Direction.Up);
            var topLeftCell = Face.MoveFewSteps(edgeSteps,topCenterCell,Direction.Left);

            for (int y = 0; y < Face.Size; y++)
            {
                var leftCell = Face.MoveFewSteps(y, topLeftCell,Direction.Down);

                for (int x = 0; x < Face.Size; x++)
                {
                    var cell = Face.MoveFewSteps(x, leftCell, Direction.Right);

                    _cellPrinters.Add(new CellPrinter(Left + x, Top +y, cell));
                }
            }
        }
    }
}
