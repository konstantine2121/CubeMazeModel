using CubeMazeModel.Enums;
using CubeMazeModel.Model;
using CubeMazeModel.Printers;
using System;
using System.Threading;

namespace CubeMazeModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestMoveUp();   //correct
            TestMoveLeft(); //error
            TestMoveUp2();  //exception
        }

        static void TestMoveUp()
        {
            Cube cube = new Cube(3);

            var cellPrinter = new CubePrinter(20, 0, cube);

            var startCell = cube.Center;

            startCell.Visited = true;
            var steps = 80;

            var currientCell = startCell;

            for (int i = 1; i < steps; i++)
            {
                currientCell = Cube.Move(currientCell, Direction.Up);
                MoveDelayAndPrintCubeState(cellPrinter);
                if (currientCell.Visited)
                {                    
                    break;
                }
                else
                {
                    currientCell.Visited = true;                    
                }

            }
        }

        static void TestMoveUp2()
        {
            Cube cube = new Cube(3);

            var cellPrinter = new CubePrinter(20, 0, cube);

            var startCell = cube.Faces[FaceLocation.Right].Center;

            startCell.Visited = true;
            var steps = 80;

            var currientCell = startCell;

            for (int i = 1; i < steps; i++)
            {
                currientCell = Cube.Move(currientCell, Direction.Up);
                MoveDelayAndPrintCubeState(cellPrinter);
                if (currientCell.Visited)
                {
                    break;
                }
                else
                {
                    currientCell.Visited = true;
                }

            }
        }

        static void TestMoveLeft()
        {
            Cube cube = new Cube(3);

            var cellPrinter = new CubePrinter(20, 0, cube);

            var startCell = cube.Center;

            startCell.Visited = true;
            var steps = 80;

            var currientCell = startCell;

            for (int i = 1; i < steps; i++)
            {
                currientCell = Cube.Move(currientCell, Direction.Left);
                MoveDelayAndPrintCubeState(cellPrinter);
                if (currientCell.Visited)
                {
                    break;
                }
                else
                {
                    currientCell.Visited = true;
                }

            }
        }

        static void MoveDelayAndPrintCubeState(CubePrinter cubePrinter)
        {
            Thread.Sleep(200);
            cubePrinter.Print();
        }
    }
}
