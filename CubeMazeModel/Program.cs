using CubeMazeModel.Enums;
using CubeMazeModel.Model;
using System;

namespace CubeMazeModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cube cube = new Cube(9);

            var startCell = cube.Center;

            startCell.Visited = true;
            var moveDirection = Direction.Right;
            var steps = 80;

            var currientCell = startCell;

            for (int i = 1; i < steps; i++) 
            {
                currientCell = cube[i, 0];

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

            Console.ReadLine();
        }
    }
}
