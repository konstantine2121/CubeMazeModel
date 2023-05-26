using CubeMazeModel.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CubeMazeModel.Model
{
    internal class Cube : BaseCellContainer
    {
        private readonly Dictionary<FaceLocation, Face> _faces = new Dictionary<FaceLocation, Face>()
        {
            [FaceLocation.Front] = null,
            [FaceLocation.Back] = null,
            [FaceLocation.Right] = null,
            [FaceLocation.Left] = null,
            [FaceLocation.Top] = null,
            [FaceLocation.Bottom] = null,
        };

        public Cube(int size) : base(size)
        {
            CreateFaces();
            LinkFaces();
        }

        private void CreateFaces()
        {
            foreach (var faceLocation in _faces.Keys.ToList())
            {
                _faces[faceLocation] = new Face(Size);
            }
        }

        private void LinkFaces()
        {
            LinkFacesHorizontal();
            LinkFacesVertical();
        }

        private void LinkFacesHorizontal()
        {
            var horizontalFaceOrder = new Face[]
            {
                _faces[FaceLocation.Front],
                _faces[FaceLocation.Right],
                _faces[FaceLocation.Back],
                _faces[FaceLocation.Left],
                _faces[FaceLocation.Front]
            };

            for (int i = 0; i < horizontalFaceOrder.Length - 1; i++)
            {
                var left = horizontalFaceOrder[i];
                var right = horizontalFaceOrder[i + 1];

                LinkVertical(left, right);
            }
        }

        private void LinkFacesVertical()
        {
            var verticalFaceOrder = new Face[]
            {
                _faces[FaceLocation.Front],
                _faces[FaceLocation.Bottom],
                _faces[FaceLocation.Back],
                _faces[FaceLocation.Top],
                _faces[FaceLocation.Front]
            };

            for (int i = 0; i < verticalFaceOrder.Length - 1; i++)
            {
                var top = verticalFaceOrder[i];
                var down = verticalFaceOrder[i + 1];

                LinkHorizontal(top, down);
            }
        }

        private void LinkHorizontal(Face topFace, Face bottomFace)
        {
            var topCells = topFace.GetEdgeCells(Direction.Down);
            var downCells = bottomFace.GetEdgeCells(Direction.Up);

            for (int i = 0; i < Size; i++)
            {
                var topCell = topCells[i];
                var downCell = downCells[i];

                topCell.SetNeighbour(Direction.Down, downCell);
                downCell.SetNeighbour(Direction.Up, topCell);
            }
        }

        private void LinkVertical(Face leftFace, Face rightFace)
        {
            var leftCells = leftFace.GetEdgeCells(Direction.Right);
            var rightCells = rightFace.GetEdgeCells(Direction.Left);

            for (int i = 0; i < Size; i++)
            {
                var leftCell = leftCells[i];
                var rightCell = rightCells[i];

                leftCell.SetNeighbour(Direction.Right, rightCell);
                rightCell.SetNeighbour(Direction.Left, leftCell);
            }
        }

        public override Cell Center => _faces[FaceLocation.Front].Center;
    }
}
