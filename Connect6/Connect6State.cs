using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class Connect6State
    {
        private Player[,] board;

        public bool IsTied()
        {
            foreach (BoardPosition pos in GetPositions())
            {
                if (GetPlayer(pos) != Player.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        private Player GetPlayer(BoardPosition pos)
        {
            if (IsValid(pos))
                return board[pos.Row, pos.Column];
            return Player.Empty;
        }

        private bool IsValid(BoardPosition pos)
        {
            return pos.Row < board.GetLength(0) && pos.Row >= 0 && pos.Column < board.GetLength(1) && pos.Column > 0;
        }

        public bool IsFinal()
        {
            if (IsTied())
            {
                return true;
            }

            foreach (BoardPosition pos in GetOccupiedPositions())
            {
                if (SixInARow(pos))
                    return true;
                if (SixInAColumn(pos))
                    return true;
                if (SixInDiagonal(pos))
                    return true;
            }

        }

        private bool SixInDiagonal(BoardPosition pos)
        {

        }

        private bool SixInAColumn(BoardPosition pos)
        {

        }

        private bool SixInARow(BoardPosition pos)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GetPlayer(pos) != GetPlayer(pos.Right))
                {
                    return false;
                }
                pos = pos.Right;
            }
            return true;
        }

        private List<BoardPosition> GetOccupiedPositions()
        {
            return new List<BoardPosition>(from pos in GetPositions() where GetPlayer(pos) != Player.Empty select pos);
        }

        private List<BoardPosition> GetPositions()
        {
            List<BoardPosition> positions = new List<BoardPosition>();

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    positions.Add(new BoardPosition(row, column));
                }
            }
            return positions;
        }

        public Connect6State Apply(Connect6Move move)
        {
            return null;
        }

        public List<Connect6Move> PossibleMoves()
        {
            return null;
        }

    }


    enum Player
    {
        Black, White, Empty
    }
}
