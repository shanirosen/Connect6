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

            return false;

        }

        private bool SixInDiagonal(BoardPosition pos)
        {
			for (int i = 0; i < 5; i++)
			{
                if (GetPlayer(pos) != GetPlayer(pos.Diagonal))
				{
					return false;
				}
                pos = pos.Diagonal;
			}
			return true;
        }

        private bool SixInAColumn(BoardPosition pos)
        {
			for (int i = 0; i < 5; i++)
			{
				if (GetPlayer(pos) != GetPlayer(pos.Down))
				{
					return false;
				}
				pos = pos.Down;
			}
			return true;
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

		private List<BoardPosition> GetNonOccupiedPositions()
		{
			return new List<BoardPosition>(from pos in GetPositions() where GetPlayer(pos) == Player.Empty select pos);
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

        public List<Connect6Move> AllPossibleMoves(BoardPosition pos)
        {
           
            return null;
        }


        private List<Connect6Move> PossibleMovesRow(BoardPosition pos)
        {
            List<BoardPosition> rowPositions = GetNonOccupiedPositionsRow(pos);
            List<Connect6Move> rowMoves = new List<Connect6Move>();
            foreach(BoardPosition pos1 in rowPositions){
                foreach(BoardPosition pos2 in rowPositions)
                {
                    if(pos1!=pos2)
                    {
                        Connect6Move move = new Connect6Move(pos1,pos2);
                        rowMoves.Add(move);

                    }
                }
            }

            return rowMoves;
        }

		private List<Connect6Move> PossibleMovesColumn(BoardPosition pos)
		{
			List<BoardPosition> colPositions = GetNonOccupiedPositionsColumn(pos);
			List<Connect6Move> colMoves = new List<Connect6Move>();
			foreach (BoardPosition pos1 in colPositions)
			{
				foreach (BoardPosition pos2 in colPositions)
				{
					if (pos1 != pos2)
					{
						Connect6Move move = new Connect6Move(pos1, pos2);
						colMoves.Add(move);

					}
				}
			}

			return colMoves;
		}

        private List<Connect6Move> PossibleMovesDiagonal(BoardPosition pos)

        private List<BoardPosition> GetNonOccupiedPositionsColumn(BoardPosition pos)
        {
			List<BoardPosition> positions = GetNonOccupiedPositions();
			List<BoardPosition> positions_col = GetNonOccupiedPositions();

			foreach (BoardPosition position in positions)
			{
                if (position.Column == pos.Column)
				{
					positions_col.Add(position);
				}

			}

			return positions_col;
        }

        private List<BoardPosition> GetNonOccupiedPositionsRow(BoardPosition pos)
        {
            List<BoardPosition> positions = GetNonOccupiedPositions();
			List<BoardPosition> positions_row = GetNonOccupiedPositions();

            foreach(BoardPosition position in positions)
            {
                if(position.Row == pos.Row)
                {
                    positions_row.Add(position);
                }

            }

            return positions_row;
        }
    }


    enum Player
    {
        Black, White, Empty
    }
}
