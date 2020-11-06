using System.Collections.Generic;
using System;
using System.Linq;

namespace Sudoku_console
{
	static class AltSolver
	{
		public static Stack<(int, int, int)> Init(int[,] board)
		{
			var context = new Stack<(int, int, int)>();
			for (int j = 8; j >= 0; j--)
			{
				for (int i = 8; i >= 0; i--)
				{
					if (board[j, i] != 0)
					{
						context.Push((i, j, board[j, i]));
					}
				}
			}
			return context;
		}
		public static void Flush(Stack<(int, int, int)> context, int[,] board)
		{
			foreach (var (i, j, n) in context)
			{
				board[j, i] = n;
			}
		}
		public static void Solve(int[,] board)
		{
			var c = Init(board);
			Flush(Solve(c, c.Count), board);
		}
        private static Stack<(int, int, int)> Solve(Stack<(int, int, int)> context, int numOfPresets)
        {
            var (nextI, nextJ) = GetNextCellPos(context);
			// have traversed all cells
			while (nextI != -1 || nextJ != -1)
			{
				context.Push((nextI, nextJ, 0));
                var i = GetNextCandidate(context);

				while (i == -1)
				{
					if (context.Count == numOfPresets)
					{
						throw new InvalidOperationException("No solution");
					}
					else
					{
						context.Pop();
						i = GetNextCandidate(context);
					}
				}

				var (lastI, lastJ, _) = context.Pop();
				context.Push((lastI, lastJ, i));
				(nextI, nextJ) = GetNextCellPos(context);
			}

			return context;
        }
        private static (int, int) GetNextCellPos(Stack<(int, int, int)> context)
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (context.Any(t => t.Item1 == i && t.Item2 == j))
                    {
                        continue;
                    }
                    else
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }
        private static bool CheckColumn(Stack<(int, int, int)> context, int num)
        {
            var (lastI, _, _) = context.Peek();
			foreach (var (currI, _, currN) in context)
			{
				if (currI == lastI && currN == num)
				{
					return false;
				}
				else
				{
					continue;
				}
			}
			return true;
        }
        private static bool CheckRow(Stack<(int, int, int)> context, int num)
        {
            var (_, lastJ, _) = context.Peek();
			foreach (var (_, currJ, currN) in context)
			{
				if (currJ == lastJ && currN == num)
				{
					return false;
				}
				else
				{
					continue;
				}
			}
			return true;
        }
        private static bool CheckSquare(Stack<(int, int, int)> context, int num)
        {
            var (lastI, lastJ, _) = context.Peek();
			foreach (var (currI, currJ, currN) in context)
			{
				if (IsInSameSquare(currI, currJ, lastI, lastJ) && currN == num)
				{
					return false;
				}
				else
				{
					continue;
				}
			}
			return true;
        }
        private static bool IsInSameSquare(int pos1I, int pos1J, int pos2I, int pos2J) 
			=> pos1I / 3 == pos2I / 3 && pos1J / 3 == pos2J / 3;

        // returns next candidate of the topmost tuple
        private static int GetNextCandidate(Stack<(int, int, int)> context)
        {
			var (lastI, lastJ, lastN) = context.Peek();
			for (int i = lastN + 1; i <= 9; i++)
			{
				if (CheckColumn(context, i) && CheckRow(context, i) && CheckSquare(context, i))
				{
					return i;
				}
				else
				{
					continue;
				}
			}

			return -1;
        }
    }
}