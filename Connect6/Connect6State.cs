using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class Connect6State
    {
        private Player[,] board = new Player[,] { { Player.Black, Player.Empty, Player.Black }, { Player.White, Player.Empty, Player.White }, { Player.Empty, Player.White, Player.Empty } };
        public readonly Player currentPlayer;

        public Connect6State (Player CurrentPlayer)
        {
            currentPlayer = CurrentPlayer;
        }

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
            Player newplayer = 1 - currentPlayer;
            board[move.Pos1.Row, move.Pos1.Column] = newplayer;
            board[move.Pos2.Row, move.Pos2.Column] = newplayer;
            Connect6State newstate = new Connect6State(newplayer);
            return newstate;

        }

        ///---------

        /*private Player[,] CloneBoard(Player[,] board)
        {
            Player[,] newboard = new Player[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < newboard.GetLength(0); i++)
            {
                for (int j = 0; j < newboard.GetLength(1); j++)
                {
                    newboard[i, j] = board[i, j];
                }
            }
            return newboard;
        }*/

        public List<Connect6Move> AllPossibleMoves()
        {
            List<BoardPosition> AllPositions = GetNonOccupiedPositions();
            List<Connect6Move> AllMoves = new List<Connect6Move>();
            foreach (BoardPosition pos1 in AllPositions)
            {
                foreach (BoardPosition pos2 in AllPositions)
                {
                    Connect6Move move = new Connect6Move(pos1, pos2);

                    if (pos1 != pos2 && !IsOppositeMoveInList(AllMoves, move))
                    {
                        AllMoves.Add(move);
                    }

                }
            }

            return AllMoves;
        }


        public bool IsOppositeMoveInList(List<Connect6Move> l, Connect6Move m)
        {
            foreach (Connect6Move move in l)
            {
                if (move.IsEqual(m.OppositeMove))
                {
                    return true;
                }
            }

            return false;
        }

    }

    enum Player
    {
        Black = 1, White = 0, Empty = -1
    }
}
