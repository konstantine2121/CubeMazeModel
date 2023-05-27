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

        public IReadOnlyDictionary <FaceLocation, Face> Faces => _faces;

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
            var horizontalFaceOrder1 = new FaceLocation[]
            {
                FaceLocation.Front,
                FaceLocation.Right,
                FaceLocation.Back,
                FaceLocation.Left,
                FaceLocation.Front
            };

            var horizontalFaceOrder2 = new FaceLocation[]
            {
                FaceLocation.Top,
                FaceLocation.Right,
                FaceLocation.Bottom,
                FaceLocation.Left,
                FaceLocation.Top
            };

            LinkFacesHorizontal(horizontalFaceOrder1);
            LinkFacesHorizontal(horizontalFaceOrder2);
        }

        private void LinkFacesHorizontal(FaceLocation[] horizontalFaceOrder1)
        {
            for (int i = 0; i < horizontalFaceOrder1.Length - 1; i++)
            {
                var left = _faces[horizontalFaceOrder1[i]];
                var right = _faces[horizontalFaceOrder1[i + 1]];

                LinkVertical(left, right);
            }
        }

        private void LinkFacesVertical()
        {
            var verticalFaceOrder1 = new FaceLocation[]
            {
                FaceLocation.Front,
                FaceLocation.Bottom,
                FaceLocation.Back,
                FaceLocation.Top,
                FaceLocation.Front
            };

            var verticalFaceOrder2 = new FaceLocation[]
            {
                FaceLocation.Right,
                FaceLocation.Bottom,
                FaceLocation.Left,
                FaceLocation.Top,
                FaceLocation.Right
            };

            LinkFacesVertical(verticalFaceOrder1);
            LinkFacesVertical(verticalFaceOrder1);
        }

        private void LinkFacesVertical(FaceLocation[] verticalFaceOrder1)
        {
            for (int i = 0; i < verticalFaceOrder1.Length - 1; i++)
            {
                var top = _faces[verticalFaceOrder1[i]];
                var down = _faces[verticalFaceOrder1[i + 1]];

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
