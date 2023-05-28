using CubeMazeModel.Enums;
using CubeMazeModel.Model;
using CubeMazeModel.Printers;
using System;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace CubeMazeModel
{
    internal class Program
    {
        private const int CubeSize = 3;

        static void Main(string[] args)
        {
            MakeTest2();
        }

        static void MakeTest1()
        {

            CubeTester.TestMovement(CubeSize, FaceLocation.Front, Vector2.Zero, Direction.Up);
            CubeTester.TestMovement(CubeSize, FaceLocation.Front, Vector2.Zero, Direction.Down);


            CubeTester.TestMovement(CubeSize, FaceLocation.Right, Vector2.Zero, Direction.Up);
            CubeTester.TestMovement(CubeSize, FaceLocation.Right, Vector2.Zero, Direction.Down);
            CubeTester.TestMovement(CubeSize, FaceLocation.Left, Vector2.Zero, Direction.Up);
            CubeTester.TestMovement(CubeSize, FaceLocation.Left, Vector2.Zero, Direction.Down);
        }

        static void MakeTest2()
        {
            var startOffset = new Vector2(-1, -1);

            CubeTester.TestMovement(CubeSize, FaceLocation.Front, startOffset, Direction.Up);
            CubeTester.TestMovement(CubeSize, FaceLocation.Front, startOffset, Direction.Down);


            CubeTester.TestMovement(CubeSize, FaceLocation.Right, startOffset, Direction.Up);
            CubeTester.TestMovement(CubeSize, FaceLocation.Right, startOffset, Direction.Down);
            CubeTester.TestMovement(CubeSize, FaceLocation.Left, startOffset, Direction.Up);
            CubeTester.TestMovement(CubeSize, FaceLocation.Left, startOffset, Direction.Down);
        }

        static void MakeBruteTest()
        {
            var faces = Enum.GetValues(typeof(FaceLocation)).Cast<FaceLocation>();
            var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();

            foreach (var face in faces) 
            {
                foreach (var direction in directions) 
                {
                    CubeTester.TestMovement(CubeSize, face, Vector2.Zero, direction, 100, 100);
                }
            }
        }

    }
}
