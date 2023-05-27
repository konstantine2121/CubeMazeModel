using CubeMazeModel.Enums;
using CubeMazeModel.Model;
using System;

namespace CubeMazeModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cube cube = new Cube(3);

            var startCell = cube.Center;

            startCell.Visited = true;
            var steps = 80;

            var currientCell = startCell;

            for (int i = 1; i < steps; i++) 
            {
                currientCell = Cube.Move(currientCell, Direction.Up);

                if (currientCell.Visited)
                {
                    Console.WriteLine("Тупик.");
                    break;
                }
                else
                {
                    currientCell.Visited = true;
                    Console.WriteLine("Шаг "+ i);
                }

            }

            var cellTopCenter = cube.Faces[FaceLocation.Right].Center;
            currientCell = cellTopCenter;
            steps = 40;

            for (int i = 1; i < steps; i++)
            {
                currientCell = Cube.Move(currientCell, Direction.Left);

                if (currientCell.Visited)
                {
                    Console.WriteLine("Тупик.");
                    break;
                }
                else
                {
                    currientCell.Visited = true;
                    Console.WriteLine("Шаг " + i);
                }

            }

            Console.ReadLine();
        }
    }
}
