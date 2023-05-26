using CubeMazeModel.Enums;
using System;
using System.Collections.Generic;

namespace CubeMazeModel.Model
{
    internal class Cell
    {
        private readonly Dictionary<Direction, Cell> _neighbours = new Dictionary<Direction, Cell>()
        {
            [Direction.Up] = null,
            [Direction.Right] = null,
            [Direction.Down] = null,
            [Direction.Left] = null
        };

        public object Useful;
        public bool Visited;
        
        public IReadOnlyDictionary<Direction, Cell> Neighbours => _neighbours;

        public void SetNeighbour(Direction direction, Cell cell)
        {
            _neighbours[direction] = cell;
        }
    }
}
