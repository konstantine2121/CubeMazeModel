using System;
using System.Collections.Generic;
using System.Linq;
using CubeMazeModel.Enums;
using System.Numerics;

namespace CubeMazeModel.Model
{
    internal class Face : BaseCellContainer
    {
        public Face(int size) : base(size)
        {
            var cells = InitializeCells();

            Center = cells[Size / 2, Size / 2];
        }

        public override Cell Center { get; }

        /// <summary>
        /// Получить ячейки с края. <br/>
        /// Направление отсчета слева-направо / сверху-вниз<br/>
        /// </summary>
        /// <param name="edgeSide">Положение грани.</param>
        /// <returns></returns>
        public IReadOnlyList<Cell> GetEdgeCells(Direction edgeSide)
        {
            var horisontalEdge = edgeSide == Direction.Up || edgeSide == Direction.Down;
            var collectionDirection = horisontalEdge ? Direction.Right : Direction.Down;

            var stepsToEdge = Size / 2;
            var centerEdgeCell = MoveFewSteps(stepsToEdge, Center, edgeSide);
            
            var start = MoveFewSteps(stepsToEdge, centerEdgeCell, horisontalEdge ? Direction.Left : Direction.Up);

            return GetCells(start, collectionDirection);
        }

        private IReadOnlyList<Cell> GetCells(Cell origin, Direction direction)
        {
            var cells = new List<Cell>();
            var current = origin;

            for (var i = 0; i < Size; i++)
            {
                cells.Add(current);
                current = Move(current, direction);
            }

            return cells;
        }

        #region InitializeCells
        
        private Cell[,] InitializeCells()
        {   
            var cells = new Cell[Size, Size];

            CreateCells(cells);
            LinkCells(cells);
            return cells;
        }

        private void CreateCells(Cell[,] cells)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    cells[y, x] = new Cell();
                }
            }
        }

        private void LinkCells(Cell[,] cells)
        {
            var move = new Dictionary<Direction, Vector2>()
            {
                [Direction.Up] = new Vector2(0, -1),
                [Direction.Right] = new Vector2(1, 0),
                [Direction.Down] = new Vector2(0, 1),
                [Direction.Left] = new Vector2(-1, 0)
            };

            Predicate<Vector2> outOfRange = 
                (point) => 
                point.X <0 || point.X >= cells.GetLength(1) 
                || point.Y < 0 || point.Y >= cells.GetLength(0);

            Action<Cell, Vector2, Direction> link = (cell, cellPosition, direction) =>
            {
                var targetPoint = cellPosition + move[direction];

                if (! outOfRange( targetPoint))
                {
                    var neighbour = cells[(int)targetPoint.Y, (int)targetPoint.X];
                    cell.SetNeighbour(direction, neighbour);
                }
            };

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    var cell = cells[y, x];
                    var point = new Vector2(x, y);

                    var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();

                    foreach (var direction in directions)
                    {
                        link(cell, point, direction);
                    }
                }
            }
        }

        #endregion InitializeCells
    }
}
