namespace CubeMazeModel.Printers
{
    internal abstract class BasePrinter
    {
        /// <summary>
        /// Область вывода на экране в консоли.
        /// </summary>
        /// <param name="left">Начало области по горизонтали.</param>
        /// <param name="top">Начало области по вертикали.</param>
        protected BasePrinter(int left, int top)
        {
            Left = left;
            Top = top;
        }

        /// <summary>
        /// Начало области по горизонтали.
        /// </summary>
        public int Left { get; }

        /// <summary>
        /// Начало области по вертикали.
        /// </summary>
        public int Top { get; }

        public abstract void Print();
    }
}
