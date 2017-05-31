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
                { Player.Empty, Player.White, Player.Black, Player.White, Player.Empty, Player.Black  },
                { Player.Black, Player.Empty, Player.Empty, Player.White, Player.Black, Player.Black  },
                { Player.Black, Player.Black, Player.White, Player.White, Player.Black, Player.Empty  },
                { Player.Black, Player.White, Player.Black, Player.Black, Player.Black, Player.White  },
                { Player.Black, Player.White, Player.Empty, Player.White, Player.White, Player.Empty  },
                { Player.White, Player.Empty, Player.White, Player.White, Player.White, Player.Black  }
            };


        }

        public static double BestMove(Connect6State state)
        {
            if (state.IsFinal())
            {
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

            if(state.TotalScore().Equals(secondstate.TotalScore()))
            {
                return 0;
            }

            return state.TotalScore() > secondstate.TotalScore() ? double.PositiveInfinity : double.NegativeInfinity;

        }
            
    }





}

