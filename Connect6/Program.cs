﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect6
{
    class Program
    {
        static void Main(string[] args)
        {
            Player[,] board = new Player[,] {
                { Player.Black, Player.White, Player.Black, Player.White, Player.White, Player.White  },
                { Player.Black, Player.White, Player.Empty, Player.Empty, Player.Black, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Black, Player.Black, Player.Empty, Player.Black  },
                { Player.White, Player.White, Player.Empty, Player.Black, Player.Black, Player.White  },
                { Player.Black, Player.White, Player.Empty, Player.Empty, Player.White, Player.Empty  },
                { Player.White, Player.Empty, Player.White, Player.White, Player.Empty, Player.Black  }
            };

            Connect6State state = new Connect6State(Player.White, board);

            //Console.WriteLine(BestMove(state));

            BoardPosition pos = new BoardPosition(2, 0);

            //Console.WriteLine(GetBestMove(state).Item1 + " " + GetBestMove(state).Item2);
            //Console.WriteLine(GetBestMoveDepthLimited(state,1));
            //Console.WriteLine(state.TotalScore());
            //Console.WriteLine(state.PossibleMovesCount());

            Console.WriteLine(GetBestMoveDepthLimited(state,4));

            /*
            Console.WriteLine(state.IsTied());
            Console.WriteLine(state.IsSix());
            Console.WriteLine(state.IsSixPos());
            Console.WriteLine("////");
            Console.WriteLine(state.SixInARow(pos));
            Console.WriteLine("//////");*/

        }



        public static double BestMove(Connect6State state)
        {
            if (state.IsFinal())
            {
                if (state.IsSix(state.currentPlayer))
                {
                    return double.PositiveInfinity;
                }
                else
                {
                    return state.IsTied() ? 0 : double.NegativeInfinity;
                }
            }

            double bestscore = double.NegativeInfinity;

            foreach (Connect6Move move in state.AllPossibleMoves())
            {
                double score = -BestMove(state.Apply(move));

                if (score > bestscore)
                {
                    bestscore = score;
                }
            }
            return bestscore;
        }



		 public static Tuple<Connect6Move, double> GetBestMove(Connect6State state)
		  {
			  if (state.IsFinal())
			  {
                if (state.IsSix(state.currentPlayer))
					  return new Tuple<Connect6Move, double>(null, double.PositiveInfinity);
				  if (state.IsTied())
					  return new Tuple<Connect6Move, double>(null, 0);
				  else
					  return new Tuple<Connect6Move, double>(null, double.NegativeInfinity);

			  }

			  double bestscore = double.NegativeInfinity;
			  Connect6Move bestmove = new Connect6Move(null, null);

			  foreach (Connect6Move move in state.AllPossibleMoves())
			  {
                double score = -GetBestMove(state.Apply(move)).Item2;
				  if (score > bestscore)
				  {
					  bestscore = score;
					  bestmove = move;
				  }
			  }
			  return new Tuple<Connect6Move, double>(bestmove, bestscore);
		  }


        public static Tuple<Connect6Move, double> GetBestMoveDepthLimited(Connect6State state, int depth)
		{
			if (state.IsFinal())
			{
				if (state.IsSix(state.currentPlayer))
					return new Tuple<Connect6Move, double>(null, double.PositiveInfinity);
				if (state.IsTied())
					return new Tuple<Connect6Move, double>(null, 0);
				else
					return new Tuple<Connect6Move, double>(null, double.NegativeInfinity);

			}
			if (depth == 0)
			{
                return new Tuple<Connect6Move, double>(null, Evaluation(state));
			}

			double bestscore = double.NegativeInfinity;
			Connect6Move bestmove = new Connect6Move(null, null);

			foreach (Connect6Move move in state.AllPossibleMoves())
			{
                double score = -GetBestMoveDepthLimited(state.Apply(move),depth-1).Item2;
				if (score > bestscore)
				{
					bestscore = score;
					bestmove = move;
				}
			}
			return new Tuple<Connect6Move, double>(bestmove, bestscore);
		}

		public static double BestMoveDepthLimited(Connect6State state, int depth)
        {
            if (state.IsFinal())
            {
				if (state.IsSix(state.currentPlayer))
				{
					return double.PositiveInfinity;
				}
				else
				{
					return state.IsTied() ? 0 : double.NegativeInfinity;
				}
            }
            if (depth == 0)
            {
                return Evaluation(state);
            }

            double bestscore = double.NegativeInfinity;

            foreach (Connect6Move move in state.AllPossibleMoves())
            {
                double score = -BestMoveDepthLimited(state.Apply(move), depth - 1);
                if (score > bestscore)
                {
                    bestscore = score;
                }
            }
            return bestscore;
        }

        private static double Evaluation(Connect6State state)
        {
            Connect6State secondstate = new Connect6State(state.currentPlayer.Next(), state.board);
            /*if (state.TotalScore().Equals(secondstate.TotalScore()))
                return 0;
            if (state.TotalScore().CompareTo(secondstate.TotalScore()) == 1)
                return double.PositiveInfinity;
            return double.NegativeInfinity;*/

            return state.TotalScore() - secondstate.TotalScore();
                
        }

    }





}

