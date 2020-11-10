namespace Sudoku_console
{
    static class Program
    {
        static void Main(string[] args)
        {
            var board = Utilities.build();
            Utilities.print(board);
            BSolver.Solve(board);
            Utilities.print(board);
        }
    }
}
