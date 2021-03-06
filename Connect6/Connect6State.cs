﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class 
    Connect6State
    {
        public readonly Player[,] board;
        public readonly Player currentPlayer;

        public Connect6State(Player CurrentPlayer, Player[,] Board)
        {
            currentPlayer = CurrentPlayer;
            board = Board;
        }

        public bool IsFull()
        {
            foreach (BoardPosition pos in GetPositions())
            {
                if (GetPlayer(pos) == Player.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsTied()
        {
            return IsFull() && !IsSix(currentPlayer) && !(IsSix(currentPlayer.Next()));

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
            if (pos != null)
            {
                return pos.Row < board.GetLength(0) && pos.Row >= 0 && pos.Column < board.GetLength(1) && pos.Column >= 0;
            }
            return false;
        }

        public bool IsFinal()
        {
            return IsFull() || IsSix(currentPlayer) || IsSix(currentPlayer.Next());

        }

        public bool IsSix(Player player)
        {
            foreach (BoardPosition pos in GetOccupiedPositions())
            {
                if (SixInARow(pos,player))
                    return true;
                if (SixInAColumn(pos,player))
                    return true;
                if (SixInDiagonal(pos,player))
                    return true;
            }
            return false;
        }

      /*  public BoardPosition IsSixPos()
		{
			foreach (BoardPosition pos in GetOccupiedPositions())
			{
				if (SixInARow(pos))
                    return pos;
				if (SixInAColumn(pos))
                    return pos;
				if (SixInDiagonal(pos))
					return pos;
			}
            return null;
		}*/


        public bool SixInDiagonal(BoardPosition pos, Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player != GetPlayer(pos))
					return false;
                if (player != GetPlayer(pos.Diagonal))
                {
                    return false;
                }
                pos = pos.Diagonal;
            }
            return true;
        }


        public bool SixInAColumn(BoardPosition pos, Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player != GetPlayer(pos))
					return false;
                if (player != GetPlayer(pos.Down))
                {
                    return false;
                }
                pos = pos.Down;
            }
            return true;
        }

        public bool SixInARow(BoardPosition pos, Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player != GetPlayer(pos))
                    return false;
                if (player != GetPlayer(pos.Right))
                {
                    return false;
                }
                pos = pos.Right;
            }
            return true;
        }


		//NEED TO MAKE SURE THE SEQUECE IS OPEN!!!!
		//l - length of sequence
		//k- number of players on endes

		public double SequenceInAColumnScore(BoardPosition pos)
        {
            double sequencelength = 0;
            BoardPosition last = new BoardPosition(pos.Row, pos.Column);
            if (GetPlayer(pos) == Player.Empty)
            {
                for (BoardPosition current = pos.Down;
                     IsValid(current) && GetPlayer(current) == currentPlayer;
                     current = current.Down)
                {
                    sequencelength++;
                    last = current;
                }

            }
            if (sequencelength == 0)
            {
                return 0;
            }
            double numofblockers = NumberOfBlockers(last.Down);
            double finalscore = Math.Pow(2, sequencelength) * (2 - numofblockers);
            return finalscore;
        }

        public double NumberOfBlockers(BoardPosition pos)
        {
            if (!(IsValid(pos)))
                return 1;
            if (board[pos.Row, pos.Column] == Player.Empty)
                return 0;
            return 1;
        }

        private double SequenceInARowScore(BoardPosition pos)
        {
            double sequencelength = 0;
            BoardPosition last = new BoardPosition(pos.Row, pos.Column);
            if (GetPlayer(pos) == Player.Empty)
            {
                for (BoardPosition current = pos.Right;
                     IsValid(current) && GetPlayer(current) == currentPlayer;
                     current = current.Right)
                {
                    sequencelength++;
                    last = current;
                }
            }
            if (sequencelength == 0)
            {
                return 0;
            }

            double numofblockers = NumberOfBlockers(last.Right);
			double finalscore = Math.Pow(2, sequencelength) * (2 - numofblockers);
			return finalscore;
        }

        private double SequenceInDiagonalScore(BoardPosition pos)
        {
            double sequencelength = 0;
            BoardPosition last = new BoardPosition(pos.Row, pos.Column);
            if (GetPlayer(pos) == Player.Empty)
            {
                for (BoardPosition current = pos.Diagonal;
                     IsValid(current) && GetPlayer(current) == currentPlayer;
                     current = current.Diagonal)
                {
                    sequencelength++;
                    last = current;
                }
            }
            if (sequencelength == 0)
            {
                return 0;
            }
            double numofblockers = NumberOfBlockers(last.Diagonal);
			double finalscore = Math.Pow(2, sequencelength) * (2 - numofblockers);

			return finalscore;
        }


        public double TotalScore()
        {
            double score = 0;
            foreach (BoardPosition pos in GetPositions())
            {
                score += SequenceInAColumnScore(pos);
                score += SequenceInARowScore(pos);
                score += SequenceInDiagonalScore(pos);
            }


            return score;
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
            Player[,] newboard = CloneBoard();

            if (IsValid(move.Pos1) && IsValid(move.Pos2))
            {
                newboard[move.Pos1.Row, move.Pos1.Column] = currentPlayer;
                newboard[move.Pos2.Row, move.Pos2.Column] = currentPlayer;
            }

            Connect6State newstate = new Connect6State(currentPlayer.Next(), newboard);
            return newstate;

        }

        ///---------

        private Player[,] CloneBoard()
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
        }

        public List<Connect6Move> AllPossibleMoves()
        {
            List<BoardPosition> AllPositions = GetNonOccupiedPositions();
            List<Connect6Move> AllMoves = new List<Connect6Move>();
            if(AllPositions.Count==1)
            {
                AllMoves.Add(new Connect6Move(AllPositions[0],AllPositions[0]));
            }
            foreach (BoardPosition pos1 in AllPositions)
            {
                foreach (BoardPosition pos2 in AllPositions)
                {
                    Connect6Move move = new Connect6Move(pos1, pos2);

                    if (pos1 != pos2 && !AllMoves.Contains(move))
                    {
                        AllMoves.Add(move);
                    }

                }
            }

            return AllMoves;
        }

        public int CountPossibleMoves()
        {
            return AllPossibleMoves().Count;
        }

    }


    public enum Player
    {
        Black = 1, White = 0, Empty = -1
    }

    public static class PlayerMethods
    {
        public static Player Next(this Player player)
        {
            switch (player)
            {
                case Player.Black:
                    return Player.White;
                case Player.White:
                    return Player.Black;
                default:
                    return Player.Empty;
            }
        }
    }
}
