using CubeMazeModel.Enums;
using CubeMazeModel.Model;
using CubeMazeModel.Printers;
using System;
using System.Numerics;
using System.Threading;

namespace CubeMazeModel
{
    internal class CubeTester
    {
        /// <summary>
        /// Провести тест на перемещение по гряням куба.
        /// </summary>
        /// <param name="cubeSize">Размер грани куба.</param>
        /// <param name="faceLocation">С какой грании стартуем.</param>
        /// <param name="centerOffset">Смещение относительно центра грани.</param>
        /// <param name="moveDirection">Направление перемещения.</param>
        /// <param name="numberOfSteps">Количество шагов до остановки(если не наступим на уже пройденную ячейку).</param>
        public static void TestMovement(int cubeSize, FaceLocation faceLocation, Vector2 centerOffset, Direction moveDirection, int numberOfSteps = 100, int millisecondsStepDelay = 500)
        {
            Cube cube = new Cube(cubeSize);

            var cellPrinter = new CubePrinter(20, 0, cube);

            var startCell = GetStartCell(cube, faceLocation, centerOffset);

            startCell.Visited = true;

            var currientCell = startCell;

            for (int i = 1; i < numberOfSteps; i++)
            {
                currientCell = Cube.Move(currientCell, moveDirection);
                MoveDelayAndPrintCubeState(cellPrinter, millisecondsStepDelay);
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

        private static Cell GetStartCell(Cube cube, FaceLocation faceLocation, Vector2 centerOffset)
        {
            var centerCell = cube.Faces[faceLocation].Center;
            var moveVertical = Cube.MoveFewSteps((int)Math.Abs(centerOffset.Y), centerCell, centerOffset.Y > 0 ? Direction.Up : Direction.Down);
            var moveHorizontal = Cube.MoveFewSteps((int)Math.Abs(centerOffset.X), moveVertical, centerOffset.X > 0 ? Direction.Right : Direction.Left);

            return moveHorizontal;
        }

        private static void MoveDelayAndPrintCubeState(CubePrinter cubePrinter, int millisecondsTimeout = 500)
        {
            Thread.Sleep(millisecondsTimeout);
            cubePrinter.Print();
        }
    }
}
