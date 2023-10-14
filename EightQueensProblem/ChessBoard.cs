using System.Collections;
using System.Text;

namespace EightQueens
{
    public class ChessBoard
    {
        private BitArray _cells;
        private byte _queenCount;
        private readonly byte _size;
        public ChessBoard(byte size, byte queens, BitArray? cells = null)
        {
            _size = size;
            _cells = cells ?? new BitArray(size * size);
            _queenCount = queens;
        }
        public static List<ChessBoard> Solve(byte size)
        {
            var board = new ChessBoard(size, 0);
            var solutions = new List<ChessBoard>();
            SolveRecursive(size, 0, board, solutions);
            return solutions;
        }

        public static void SolveRecursive(byte size, byte column, ChessBoard board, List<ChessBoard> solutions)
        {
            if (column >= size)
            {
                if (board.QueenCount() == size)
                {
                    solutions.Add(board);
                    return;
                }
            }
            
            for (byte row = 0; row < size; row++)
            {
                var canPlaceQueen = board.TrySetQueen(row, column);
                if (canPlaceQueen)
                {
                    var newSate = board.Clone().SetQueen(row, column);
                    SolveRecursive(size, (byte)(column+1), newSate, solutions);
                }
            }
        }

        public ChessBoard Clone() => new(_size, _queenCount, (BitArray)_cells.Clone());
        public void Reset() { _queenCount = 0; _cells = new BitArray(_size * _size); }
        public byte QueenCount() => _queenCount;
        public ChessBoard SetQueen(byte column, byte row)
        {
            _cells[row * _size + column] = true;
            _queenCount++;
            return this;
        }
        public bool TrySetQueen(byte column, byte row)
        {
            if (_cells[row * _size + column]) return false;
            //Spiral checks
            if (_queenCount >= 1)
            {
                for (int index = 0; index < _size; index++)
                {
                    //Right
                    if (column + index < _size)
                    {
                        var rightIndex = row * _size + (column + index);
                        if (_cells[rightIndex])
                        {
                            return false;
                        }
                    }
                    //Down & Right
                    if (row + index < _size && column + index < _size)
                    {
                        var downRightIndex = (row + index) * _size + (column + index);
                        if (_cells[downRightIndex])
                        {
                            return false;
                        }
                    }
                    //Down
                    if (row + index < _size)
                    {
                        var downIndex = (row + index) * _size + column;
                        if (_cells[downIndex])
                        {
                            return false;
                        }
                    }
                    //Down & Left
                    if (row + index < _size && column - index >= 0)
                    {
                        var downLeftIndex = (row + index) * _size + (column - index);
                        if (_cells[downLeftIndex])
                        {
                            return false;
                        }
                    }
                    //Left
                    if (column - index >= 0)
                    {
                        var leftIndex = row * _size + (column - index);
                        if (_cells[leftIndex])
                        {
                            return false;
                        }
                    }
                    //Up And Left
                    if (row - index >= 0 && column - index >= 0)
                    {
                        var upLeftIndex = (row - index) * _size + (column - index);
                        if (_cells[upLeftIndex])
                        {
                            return false;
                        }
                    }
                    //Up
                    if (row - index >= 0)
                    {
                        var upIndex = (row - index) * _size + column;
                        if (_cells[upIndex])
                        {
                            return false;
                        }
                    }
                    //Up And Right
                    if (row - index >= 0 && column + index < _size)
                    {
                        var upAndRightIndex = (row - index) * _size + (column + index);
                        if (_cells[upAndRightIndex])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (byte r = 0; r < _size; r++)
            {
                for (byte c = 0; c < _size; c++)
                {
                    var cell = _cells[r * _size + c] ? "Q " : ". ";
                    builder.Append($"{cell}");
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

    }
}
