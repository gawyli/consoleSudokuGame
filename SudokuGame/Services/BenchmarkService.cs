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
        public static void SolverBenchmark(int[,] board, int level, int boardSize)
        {
            var stopwatch = new Stopwatch();
            var backtrackingSolver = new Backtracking(board);

            if (boardSize == 16 && level > 2)
            {
                stopwatch.Start();
                ConstraintProg.SolveBoard(board);
                stopwatch.Stop();

                Console.WriteLine("\n===Benchmark===");
                Console.WriteLine("Constraint Programming: " + stopwatch.Elapsed + "\n");
                Console.WriteLine("Press enter to continue..");
            }
            else
            {
                stopwatch.Start();
                backtrackingSolver.SolveSudoku();
                stopwatch.Stop();

                Console.WriteLine("\n===Benchmark===");
                Console.WriteLine("Backtracking: " + stopwatch.Elapsed);
                stopwatch.Restart();

                stopwatch.Start();
                ConstraintProg.SolveBoard(board);
                stopwatch.Stop();

                Console.WriteLine("Constraint Programming: " + stopwatch.Elapsed + "\n");
                Console.WriteLine("Press enter to continue..");
            }
            
            
            Console.ReadLine();
        }
    }
}
