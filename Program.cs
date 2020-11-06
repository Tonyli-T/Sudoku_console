using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sudoku_console
{
    class Program
    {
        // Printing the puzzle.
        private static void print(int[,] board) 
        {
            for (int i = 0; i < board.GetLength(1); i++)
            {
                Console.WriteLine("-------------------");
                Console.Write("|");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                    {
                        Console.Write(" |");       
                    } 
                    else 
                    {
                        Console.Write(board[i, j] + "|");       
                    }
                }
                Console.WriteLine("");
            }

            Console.WriteLine("-------------------"); 
        }
        // Creating a sudoku board based on the board.
        private static int[,] build()
        {
            Console.WriteLine("Please indicate the correct file path for the puzzle: ");
            string fs = Console.ReadLine();
            List<string[]> ss = new List<string[]>();
            // using (var sr = new StreamReader(fs))
            // {
            //     while (!sr.EndOfStream)
            //     {
            //         var current = sr.ReadLine();
            //         ss.Add(current.Split(", "));
            //     }
            // }
            // throw new NotImplementedException();
            // ┏━━┯━┓┛┗┃━─│┯┨┠┷┼
            // ┃ 0│
            // ┠─━┼─┨
            // ┗━┷━┛


            // ./puzzles/p1.txt


            // Loading the puzzle
            Boolean jugg = true;
            while (jugg) {
                try 
                {
                    ss = File.ReadAllLines("./puzzles/p1.txt").Select(s => s.Split(", ")).ToList();
                    jugg = false;
                }
                catch (FileNotFoundException) 
                {
                    Console.WriteLine("Sorry, the file does not exist. Can you type it again? ");
                    fs = Console.ReadLine();    
                }
            }
            
            int[,] board = new int[9,9];
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    try
                    {
                        board[j, i] = int.Parse(ss[j][i]);
                    } 
                    catch (FormatException)
                    {
                        Console.Write("Incorrect file format. Terminating......");
                        System.Environment.Exit(1);
                    }
                }
            }
            
            return board;
        }

        // Using back tracking algorithm to solve the puzzle
        private static void solve(int[,] board)
        {
            Console.WriteLine("Press any key to solve your puzzle");
            Console.ReadKey(true);

            Stack<(int, int, int)> tracker = new Stack<(int, int, int)>();
            // Experimenting outcomes.
            for (int i = 0; i < board.GetLength(1); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // When we step into the realm of "0".                 
                    if (board[i, j] == 0 || (tracker.Where(t => t.Item1 == i && t.Item2 == j).Count() != 0))
                    {
                        bool jugg = true;
                        bool refil = false;
                        int num;

                        if (board[i, j] == 0)
                        {
                            num = 1;
                        }
                        else 
                        {
                            num = board[i, j];
                        }

                        while (jugg) 
                        {
                            // The rules that define how rows and columns behave+
                            for (int k = 0 ; k < 9; k++) 
                            {
                                if ((board[k, j] == num) || (board[i, k] == num))
                                {
                                    jugg = false;
                                }
                            }

                            if (!jugg)
                            {
                                // When we tried all possible nums, and it is still incorrect.
                                if (num == 9)
                                {
                                    board[i, j] = 0;
                                    (int, int, int) track = tracker.Pop();
                                    i = track.Item1;
                                    j = track.Item2;
                                    board[i , j] = track.Item3 + 1;    
                                    refil = true;      
                                                
                                    break;
                                }

                                num++;
                                jugg = true;
                            }
                            else 
                            {
                                board[i, j] = num;                              
                                jugg = false;
                            }
                        }

                        // After we fill the number successfully, we record the value of i, j and num.
                        tracker.Push((i, j, board[i, j]));
                        
                        if (refil)
                        {
                            j--;
                        }
                    } 
                }
            }
        }

        static void Main(string[] args)
        {
            var board = build();
            print(board);
            AltSolver.Solve(board);
            //solve(board);
            print(board);
        }
    }
}
