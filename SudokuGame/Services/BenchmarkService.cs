using SudokuGame.Solvers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.Services
{
    public class BenchmarkService
    {
        public static void SolverBenchmark(int[,] board)
        {
            var stopwatch = new Stopwatch();
            var backtrackingSolver = new Backtracking(board);

            stopwatch.Start();
            backtrackingSolver.SolveSudoku();
            stopwatch.Stop();

            Console.Clear();
            Console.WriteLine("Backtracking: " + stopwatch.Elapsed + "\n");
            stopwatch.Restart();

            stopwatch.Start();
            ConstraintProg.SolveBoardMin(board);
            stopwatch.Stop();

            Console.WriteLine("Constraint Programming - MinValue: " + stopwatch.Elapsed);
            stopwatch.Restart();

            stopwatch.Start();
            ConstraintProg.SolveBoardMax(board);
            stopwatch.Stop();

            Console.WriteLine("Constraint Programming - MaxValue: " + stopwatch.Elapsed + "\n");
            Console.WriteLine("Press enter to continue..");
            
            Console.ReadLine();
        }
    }
}
