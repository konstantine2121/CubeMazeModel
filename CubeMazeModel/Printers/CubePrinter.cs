using CubeMazeModel.Enums;
using CubeMazeModel.Model;
using System.Collections.Generic;

namespace CubeMazeModel.Printers
{
    internal class CubePrinter : BasePrinter
    {
        private readonly List<FacePrinter> _facePrinters = new List<FacePrinter>();

        public CubePrinter(int left, int top, Cube cube) : base(left, top)
        {
            Cube = cube;
            InitializePrinters();
        }

        public Cube Cube { get; }

        public override void Print()
        {
            _facePrinters.ForEach(f => f.Print());
        }

        private void InitializePrinters()
        {
            _facePrinters.Clear();

            var faceSize = Cube.Size;
            var space = 2;
            var yOffset = space+Top;
            var xOffset = space+Left;

            var centerX = xOffset + faceSize + space;
            var back = new FacePrinter(
                centerX,
                yOffset,
                Cube.Faces[FaceLocation.Back]);

            yOffset += faceSize + space;

            var top = new FacePrinter(
                centerX,
                yOffset,
                Cube.Faces[FaceLocation.Top]);

            yOffset += faceSize + space;

            var left = new FacePrinter(
                xOffset,
                yOffset,
                Cube.Faces[FaceLocation.Left]);

            var front = new FacePrinter(
                centerX,
                yOffset,
                Cube.Faces[FaceLocation.Front]);

            var right = new FacePrinter(
                centerX+faceSize+space,
                yOffset,
                Cube.Faces[FaceLocation.Right]);

            yOffset += faceSize + space;

            var bottom = new FacePrinter(
                centerX,
                yOffset,
                Cube.Faces[FaceLocation.Bottom]);

            _facePrinters.AddRange(new FacePrinter[] { back, top, left, front, right, bottom });
        }
    }
}
