using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.ConstraintSolver;

namespace SudokuGame.New
{
    public class SolverGame
    {

        public static void NewSolver(int[,] puzzle)
        {
            Solver solver = new Solver("SudokuSolver");

            // Define the variables
            IntVar[,] board = solver.MakeIntVarMatrix(9, 9, 1, 9, "board");

            // Define the constraints for rows and columns
            for (int i = 0; i < 9; i++)
            {
                solver.Add((from j in Enumerable.Range(0, 9) select board[i, j]).ToArray().AllDifferent());
                solver.Add((from j in Enumerable.Range(0, 9) select board[j, i]).ToArray().AllDifferent());
            }

            // Define the constraints for the 3x3 boxes
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    solver.Add((from x in Enumerable.Range(i * 3, 3) from y in Enumerable.Range(j * 3, 3) select board[x, y]).ToArray().AllDifferent());
                }
            }


            // Add the initial values as constraints
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle[i, j] != 0)
                    {
                        solver.Add(board[i, j] == puzzle[i, j]);
                    }
                }
            }
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            // Search for a solution
            DecisionBuilder db = solver.MakePhase(board.Flatten(), Solver.INT_VAR_SIMPLE, Solver.ASSIGN_MIN_VALUE);
            solver.NewSearch(db);
            if (solver.NextSolution())
            {
                stopwatch.Stop();
                Console.WriteLine("Constrains: " + stopwatch.Elapsed.ToString());
                // Print the solution
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Console.Write(board[i, j].Value() + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No solution found.");
            }
        }
    }
}
