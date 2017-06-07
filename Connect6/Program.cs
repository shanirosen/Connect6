using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Connect6
{
    class Program
    {

        static void Main(string[] args)
        {
            /*Player[,] board = new Player[,] {
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  }
            };*/

            PlayGame();


        }
		public static void PlayGameComputer()
		{
			Player[,] board = new Player[,] {
				{ Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
				{ Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
				{ Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
				{ Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
				{ Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
				{ Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  }
			};

			Connect6State state = new Connect6State(Player.White, board);

			while (!state.IsFinal())
			{
				Console.WriteLine("Current Player: " + state.currentPlayer);
                Console.WriteLine();
                PrintBoard(board);
				if (state.currentPlayer == Player.White)
				{
					Console.WriteLine("Calculating Move...");					
                    Console.WriteLine();
                    Connect6Move move = GetBestMoveDepthLimited(state, 2).Item1;
					state = state.Apply(move);
				}
				else
				{
					Console.WriteLine("Calculating Move...");
					Console.WriteLine();
                    Connect6Move move = GetBestMoveDepthLimited(state, 2).Item1;
					state = state.Apply(move);
				}

				board = state.board;

			}

			PrintBoard(board);
			if (state.IsTied())
			{
				Console.WriteLine("Tied!");
			}
			else
			{
				Console.WriteLine("{0} Won!", state.currentPlayer.Next());
			}


		}


		public static void PlayGame()
        {
			Player[,] board = new Player[,] {
				{ Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  },
                { Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty, Player.Empty  }
			};

            Connect6State state = new Connect6State(Player.White, board);

            Console.WriteLine("The Computer is White, You Are Black");
            while (!state.IsFinal())
            {
                Console.WriteLine("Current Player: " + state.currentPlayer);
                PrintBoard(board);
                if (state.currentPlayer == Player.White)
                {
                    Console.WriteLine("Calculating Move...");
                    Connect6Move move = GetBestMoveDepthLimited(state, 2).Item1;
                    state = state.Apply(move);
                }
                else{
                    Console.WriteLine("Choose A Row And A Column for position 1:");
                    int row = int.Parse(Console.ReadLine());
                    int col = int.Parse(Console.ReadLine());
                    BoardPosition pos1 = new BoardPosition(row, col);
                    Console.WriteLine("Choose A Row And A Column for position 2:");
					int row2 = int.Parse(Console.ReadLine());
					int col2 = int.Parse(Console.ReadLine());
                    BoardPosition pos2 = new BoardPosition(row2, col2);
                    Connect6Move move = new Connect6Move(pos1, pos2);
                    state = state.Apply(move);
                }

                board = state.board;
        
            }

            PrintBoard(board);

            if (state.IsTied())
            {
                Console.WriteLine("Tied!");
            }
            else
            {
                Console.WriteLine("{0} Won!",state.currentPlayer.Next());
            }


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
                double score = -GetBestMoveDepthLimited(state.Apply(move), depth - 1).Item2;
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

            return state.TotalScore() - secondstate.TotalScore();

        }

        public static void PrintBoard(Player[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
			{
                for (int j = 0; j < board.GetLength(1); j++)
				{
                    if(board[i,j]==Player.White)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
						
					}
                    if(board[i,j]==Player.Black)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;

					}
                    Console.Write(string.Format("["+i+","+j+"] " + "{0} ", board[i, j]));
					Console.ResetColor();

				}
				Console.Write(Environment.NewLine + Environment.NewLine);
			}
        }


        public static Player[,] GenerateBoard(string str)
        {
            Char seperator = '\n';
            String[] substrings = str.Split(seperator);
            Player[,] board = new Player[substrings.Count(), substrings.Count()];
          
            for (int rows = 0; rows < board.GetLength(0); rows++)
            {
                for (int columns = 0; columns < board.GetLength(1); columns++)
                {
                    if (substrings[rows][columns] == 'X')
                    {
                        board[rows, columns] = Player.White;
                    }

                    else if (substrings[rows][columns] == 'O')
                    {
                        board[rows, columns] = Player.Black;
                    }
                    else
                    {
                        board[rows, columns] = Player.Empty;
                    }
                }
            }

            return board;
        }

    }





}

