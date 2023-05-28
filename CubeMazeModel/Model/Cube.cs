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

        public IReadOnlyDictionary<FaceLocation, Face> Faces => _faces;

        public override Cell Center => _faces[FaceLocation.Front].Center;

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
            LinkFacesCustom();
        }

        private void LinkFacesHorizontal()
        {
            var horizontalFaceOrder = new FaceLocation[]
            {
                FaceLocation.Front,
                FaceLocation.Right,
                FaceLocation.Back,
                FaceLocation.Left,
                FaceLocation.Front
            };

            LinkFacesHorizontal(horizontalFaceOrder);
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

        private void LinkFacesVertical()
        {
            var verticalFaceOrder = new FaceLocation[]
            {
                FaceLocation.Front,
                FaceLocation.Bottom,
                FaceLocation.Back,
                FaceLocation.Top,
                FaceLocation.Front
            };

            LinkFacesVertical(verticalFaceOrder);
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

        private void LinkFacesCustom()
        {
            var links = new List<LinkInfo>()
            {
                new LinkInfo(
                    this, 
                    new EdgeSelectionInfo(FaceLocation.Top, Direction.Right),
                    new EdgeSelectionInfo(FaceLocation.Right, Direction.Up, true)
                    ),
                new LinkInfo(
                    this,
                    new EdgeSelectionInfo(FaceLocation.Right, Direction.Down),
                    new EdgeSelectionInfo(FaceLocation.Bottom, Direction.Right)
                    ),
                new LinkInfo(
                    this,
                    new EdgeSelectionInfo(FaceLocation.Bottom, Direction.Left),
                    new EdgeSelectionInfo(FaceLocation.Left, Direction.Down, true)
                    ),
                new LinkInfo(
                    this,
                    new EdgeSelectionInfo(FaceLocation.Left, Direction.Up),
                    new EdgeSelectionInfo(FaceLocation.Top, Direction.Left)
                    )
            };

            links.ForEach(link => link.Link());
        }

        private struct EdgeSelectionInfo
        {
            public FaceLocation FaceLocation;
            public Direction EdgeDirection;

            /// <summary>
            /// Инвертировать направление отсчета.
            /// </summary>
            public bool InvertOrder;

            /// <summary>
            /// Информация для выборки ячеек с ребра куба.
            /// </summary>
            /// <param name="faceLocation">Грань куба.</param>
            /// <param name="edgeDirection">Ребро грани.</param>
            /// <param name="invertOrder">Инвертировать порядок ячеек.</param>
            public EdgeSelectionInfo(FaceLocation faceLocation, Direction edgeDirection, bool invertOrder = false)
            {
                FaceLocation = faceLocation;
                EdgeDirection = edgeDirection;
                InvertOrder = invertOrder;
            }
        }

        private class LinkInfo
        {
            private readonly Cube _cube;

            public LinkInfo(Cube cube, EdgeSelectionInfo firstEdgeInfo, EdgeSelectionInfo secondEdgeInfo)
            {
                _cube = cube;
                FirstEdgeInfo = firstEdgeInfo;
                SecondEdgeInfo = secondEdgeInfo;
            }

            public EdgeSelectionInfo FirstEdgeInfo { get; }
            public EdgeSelectionInfo SecondEdgeInfo { get; }

            public void Link()
            {
                var firstCells = GetEdgeCells(FirstEdgeInfo);
                var secondCells = GetEdgeCells(SecondEdgeInfo);

                for (int i = 0; i < firstCells.Count; i++)
                {
                    var cell1 = firstCells[i];
                    var cell2 = secondCells[i];

                    cell1.SetNeighbour(FirstEdgeInfo.EdgeDirection, cell2);
                    cell2.SetNeighbour(SecondEdgeInfo.EdgeDirection, cell1);
                }
            }

            private List<Cell> GetEdgeCells(EdgeSelectionInfo info)
            {
                var face = _cube.Faces[info.FaceLocation];
                var cells = new List<Cell>(face.GetEdgeCells(info.EdgeDirection));

                if (info.InvertOrder)
                {
                    cells.Reverse();
                }

                return cells;
            }
        }
    }
}
