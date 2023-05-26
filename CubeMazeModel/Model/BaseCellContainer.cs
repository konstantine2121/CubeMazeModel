using CubeMazeModel.Enums;
using System;

namespace CubeMazeModel.Model
{
    internal abstract class BaseCellContainer
    {
        public BaseCellContainer(int size)
        {
            if (size % 2 == 0)
            {
                throw new ArgumentException("size должен быть нечетным!");
            }

            Size = size;
        }

        public int Size { get; }

        public abstract Cell Center { get; }

        public Cell this[int x, int y]
        {
            get
            {
                var start = Center;
                var cell = start;

                var horizontalDirection = x > 0 ?
                    Direction.Right :
                    Direction.Left;

                var verticalDirection = y > 0 ?
                    Direction.Up :
                    Direction.Down;

                cell = MoveFewSteps(x, cell, horizontalDirection);
                cell = MoveFewSteps(y, cell, verticalDirection);

                return cell;
            }
        }

        protected Cell MoveFewSteps(int numberOfSteps, Cell origin, Direction direction)
        {
            for (int i = 0; i < Math.Abs(numberOfSteps); i++)
            {
                origin = Move(origin, direction);
            }

            return origin;
        }

        protected Cell Move(Cell cell, Direction direction)
        {
            return cell.Neighbours[direction];
        }
    }
}
