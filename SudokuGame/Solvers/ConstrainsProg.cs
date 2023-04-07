using Google.OrTools.ConstraintSolver;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solver = Google.OrTools.ConstraintSolver.Solver;

namespace SudokuGame.Solvers
{
    public class ConstrainsProg
    {
        private static int[,] solutionBoard = null!;
        public static int[,] SolveBoardMin(int[,] board)
        {
            Solver solver = new Solver("SudokuSolver");

            // Define the variables
            IntVar[,] possibleBoard = solver.MakeIntVarMatrix(9, 9, 1, 9, "board");

            SetUpConstrains(solver, possibleBoard, board);

            // Search for a solution
            DecisionBuilder db = solver.MakePhase(possibleBoard.Flatten(), Solver.INT_VAR_SIMPLE, Solver.ASSIGN_MIN_VALUE);

            solver.NewSearch(db);

            if (solver.NextSolution())
            {
                solutionBoard = new int[9, 9];

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        solutionBoard[i, j] = (int)possibleBoard[i, j].Value();
                    }
                }

                return solutionBoard;
            }

            return solutionBoard;
        }

        public static int[,] SolveBoardMax(int[,] board)
        {
            Solver solver = new Solver("SudokuSolver");

            // Define the variables
            IntVar[,] possibleBoard = solver.MakeIntVarMatrix(9, 9, 1, 9, "board");

            SetUpConstrains(solver, possibleBoard, board);

            // Search for a solution
            DecisionBuilder db = solver.MakePhase(possibleBoard.Flatten(), Solver.INT_VAR_SIMPLE, Solver.ASSIGN_MAX_VALUE);

            solver.NewSearch(db);

            if (solver.NextSolution())
            {
                solutionBoard = new int[9, 9];

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        solutionBoard[i, j] = (int)possibleBoard[i, j].Value();
                    }
                }

                return solutionBoard;
            }

            return solutionBoard;
        }

        private static void SetUpConstrains(Solver solver, IntVar[,] possibleBoard, int[,] board)
        {
            // Define the constraints for rows and columns
            for (int i = 0; i < 9; i++)
            {
                solver.Add((from j in Enumerable.Range(0, 9) select possibleBoard[i, j]).ToArray().AllDifferent());
                solver.Add((from j in Enumerable.Range(0, 9) select possibleBoard[j, i]).ToArray().AllDifferent());
            }

            // Define the constraints for the 3x3 boxes
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    solver.Add((from x in Enumerable.Range(i * 3, 3) from y in Enumerable.Range(j * 3, 3) select possibleBoard[x, y]).ToArray().AllDifferent());
                }
            }

            // Add the initial values as constraints
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != 0)
                    {
                        solver.Add(possibleBoard[i, j] == board[i, j]);
                    }
                }
            }
        }
    }
}
