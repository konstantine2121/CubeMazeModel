using System;

namespace CubeMazeModel.Model
{
    internal class CubeBuilder
    {
        public Cube Build(int faceSize) 
        { 
            if (faceSize % 2 == 0)
            {
                throw new ArgumentException("faceSize должен быть нечетным!");
            }

            return new Cube(faceSize); 
        }
    }
}
