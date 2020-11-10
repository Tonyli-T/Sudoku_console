using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sudoku_console
{
    static class Utilities {
        // Printing the puzzle.
        public static void print(int[,] board) 
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
        public static int[,] build()
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

            // Loading the puzzle
            Boolean jugg = true;
            while (jugg) {
                try 
                {
                    ss = File.ReadAllLines(fs).Select(s => s.Split(", ")).ToList();
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
    }
}