using System;
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
                { Player.Black, Player.White, Player.Black, Player.White, Player.Black, Player.Black  },
                { Player.Black, Player.White, Player.Black, Player.White, Player.Black, Player.Black  },
                { Player.Black, Player.Black, Player.Empty, Player.White, Player.Black, Player.Black  },
                { Player.Black, Player.White, Player.Black, Player.Black, Player.Black, Player.Black  },
                { Player.Black, Player.White, Player.Empty, Player.White, Player.White, Player.Empty  },
                { Player.White, Player.White, Player.White, Player.White, Player.White, Player.Black  }
            };

            Player[,] b2 = new Player[,]{
                {Player.Black,Player.Empty,Player.White},
                {Player.White,Player.Black,Player.Empty},
                {Player.White,Player.White,Player.Empty}
            };

            Connect6State state = new Connect6State(Player.Black, board);

            Connect6State state2 = new Connect6State(Player.Black, b2);

            Console.WriteLine(state.IsFinal());
            Console.WriteLine(state.IsFull());
            /*
            List<BoardPosition> bp = state.GetPositions();
            foreach(BoardPosition pos in bp)
            {
                Console.WriteLine("pos= " + pos);
                Console.WriteLine("check is six");
                Console.WriteLine(state.IsSix());
                Console.WriteLine("check smal functions");
                Console.WriteLine("(row) " + state.SixInARow(pos));
                Console.WriteLine("(col) " + state.SixInAColumn(pos));
                Console.WriteLine("(diag) " + state.SixInDiagonal(pos));

            }*/



            List<Connect6Move> possiblemoves = state.AllPossibleMoves();

            foreach(Connect6Move move in possiblemoves)
            {
                Console.WriteLine(move);
            }

            Console.WriteLine(BestMove(state));

            //BoardPosition pos1 = new BoardPosition(1, 0);
            //BoardPosition pos2 = new BoardPosition(1, 1);
            //Connect6Move m1 = new Connect6Move(pos1, pos2);
            //List<Connect6Move> ll = new List<Connect6Move>();
            //ll.Add((m1));
            //Connect6Move m2 = new Connect6Move(pos2, pos1);
            //Console.WriteLine(IsMoveInList(ll,OppositeMove(m2)));


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
            return 0;
            //check how many sequences with maximum players the enemy has

        }

        /*private static int Count(Connect6State state, Player currentPlayer)
        {
            int count = 0;
            List<BoardPosition> occupiedPos = state.GetOccupiedPositions();
            foreach (BoardPosition pos in occupiedPos)
            {
                if (state.GetPlayer(pos) == currentPlayer)
                {
                    count++;
                }

            }
            return count;
        }*/
    }





}

