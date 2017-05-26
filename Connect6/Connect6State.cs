using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class Connect6State
    {
        private Player[,] board = new Player[,] { { Player.Black, Player.Empty, Player.Empty }, { Player.Empty, Player.Empty, Player.White }, { Player.Empty, Player.White, Player.Empty } };

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

        public Player GetPlayer(BoardPosition pos)
        {
            if (IsValid(pos))
            {
                return board[pos.Row, pos.Column];
            }
            return Player.Empty;
        }

        private bool IsValid(BoardPosition pos)
        {
            return pos.Row < board.GetLength(0) && pos.Row >= 0 && pos.Column < board.GetLength(1) && pos.Column >= 0;
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

        public List<BoardPosition> GetOccupiedPositions()
        {
            return new List<BoardPosition>(from pos in GetPositions() where GetPlayer(pos) != Player.Empty select pos);
        }

        public List<BoardPosition> GetNonOccupiedPositions()
        {
            return new List<BoardPosition>(from pos in GetPositions() where GetPlayer(pos) == Player.Empty select pos);
        }

        public List<BoardPosition> GetPositions()
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

       ///////////Need to check

        public Connect6State Apply(Connect6Move move)
        {
            Player[,] newboard = CloneBoard(board);
            newboard[move.GetPos1().Row, move.GetPos1().Column] = GetPlayer(move.GetPos1());
            newboard[move.GetPos2().Row, move.GetPos2().Column] = GetPlayer(move.GetPos2());
            Connect6State state = new Connect6State();
            state.board = newboard;
            return state;

        }

        ///---------

        private Player[,] CloneBoard(Player[,] board)
        {
            Player[,] newboard = new Player[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < newboard.GetLength(0; i++)
            {
                for (int j = 0; j < newboard.GetLength(1); j++)
                {
                    newboard[i, j] = board[i, j];
                }
            }
            return newboard;
        }

        public List<Connect6Move> AllPossibleMoves()
        {
            var allMoves = PossibleMovesRow().Concat(PossibleMovesColumn()).Concat(PossibleMovesDiagonal()).ToList();

            return allMoves;
        }


        public List<Connect6Move> PossibleMovesRow()
        {
            List<BoardPosition> rowPositions = GetNonOccupiedPositions();
            List<Connect6Move> rowMoves = new List<Connect6Move>();
            foreach (BoardPosition pos1 in rowPositions)
            {
                foreach (BoardPosition pos2 in rowPositions)
                {
                    if (pos1 != pos2 && pos1.Row == pos2.Row)
                    {
                        Connect6Move move = new Connect6Move(pos1, pos2);

                        if (!IsOppositeMoveInList(rowMoves, move))
                        {
                            rowMoves.Add(move);
                        }

                    }
                }
            }

            return rowMoves;
        }


        public List<Connect6Move> PossibleMovesColumn()
        {
            List<BoardPosition> colPositions = GetNonOccupiedPositions();
            List<Connect6Move> colMoves = new List<Connect6Move>();
            foreach (BoardPosition pos1 in colPositions)
            {
                foreach (BoardPosition pos2 in colPositions)
                {
                    if (pos1 != pos2 && pos1.Column == pos2.Column)
                    {
                        Connect6Move move = new Connect6Move(pos1, pos2);
                        if (!IsOppositeMoveInList(colMoves, move))
                        {
                            colMoves.Add(move);
                        }

                    }
                }
            }

            return colMoves;
        }


        public List<Connect6Move> PossibleMovesDiagonal()
        {
            List<BoardPosition> diagPositions = GetNonOccupiedPositions();
            List<Connect6Move> diagMoves = new List<Connect6Move>();
            foreach (BoardPosition pos1 in diagPositions)
            {
                foreach (BoardPosition pos2 in diagPositions)
                {
                    if (pos1 != pos2 && pos1.Column == pos2.Column + 1 && pos1.Row == pos2.Row + 1)
                    {
                        Connect6Move move = new Connect6Move(pos1, pos2);
                        if (!IsOppositeMoveInList(diagMoves, move))
                        {
                            diagMoves.Add(move);
                        }

                    }
                }
            }

            return diagMoves;
        }

        private Connect6Move OppositeMove(Connect6Move m)
        {
            return new Connect6Move(m.GetPos2(), m.GetPos1());
        }

        public bool IsOppositeMoveInList(List<Connect6Move> l, Connect6Move m)
        {
            foreach (Connect6Move move in l)
            {
                if (AreMovesEqual(move, OppositeMove(m)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AreMovesEqual(Connect6Move m1, Connect6Move m2)
        {
            return m1.GetPos1().Row == m2.GetPos1().Row && m1.GetPos1().Column == m2.GetPos1().Column && m1.GetPos2().Row == m2.GetPos2().Row && m1.GetPos2().Column == m2.GetPos2().Column;
        }
    }

    enum Player
    {
        Black = 1, White = 0, Empty = -1
    }
}
