﻿using System;
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
                { Player.Empty, Player.White, Player.Black, Player.White, Player.White, Player.Black  },
                { Player.White, Player.Empty, Player.White, Player.White, Player.Black, Player.Black  },
                { Player.White, Player.Black, Player.Black, Player.Black, Player.Black, Player.Black  },
                { Player.White, Player.White, Player.Black, Player.Black, Player.Black, Player.White  },
                { Player.White, Player.White, Player.White, Player.White, Player.White, Player.White  },
                { Player.Black, Player.Black, Player.White, Player.White, Player.White, Player.Black  }
            };

            Connect6State state = new Connect6State(Player.White, board);
            Tuple<Connect6Move, double> f = GetBestMove(state);
            Console.WriteLine(f.Item1);
            Console.WriteLine(f.Item2);
            Console.WriteLine(BestMove(state));

            BoardPosition pos = new BoardPosition(2, 0);
            Console.WriteLine(state.GetPlayer(pos));
            Console.WriteLine("right: " + pos.Right + " " + state.GetPlayer(pos.Right));

            Console.WriteLine("down: " +pos.Down + " " + state.GetPlayer(pos.Down));
            Console.WriteLine("diag: "+pos.Diagonal + " " + state.GetPlayer(pos.Diagonal));
            Console.WriteLine(state.SixInARow(pos));
            Console.WriteLine(state.AllPossibleMoves().Count);


        }



        public static double BestMove(Connect6State state)
        {
            if (state.IsFinal())
            {
                if (state.IsSix())
                    return double.PositiveInfinity;
                else
                    return state.IsTied() ? 0 : double.NegativeInfinity;
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
				if (state.IsSix())
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
                double score = -BestMove(state.Apply(move));
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
                return state.IsTied() ? 0 : double.NegativeInfinity;
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

            if (state.TotalScore().Equals(secondstate.TotalScore()))
            {
                return 0;
            }

            return state.TotalScore() > secondstate.TotalScore() ? double.PositiveInfinity : double.NegativeInfinity;

        }

    }





}

