using Google.OrTools.ConstraintSolver;
using Solver = Google.OrTools.ConstraintSolver.Solver;

namespace SudokuGame.Solvers
{
    public class ConstraintProg
    {
        private static int[,]? _solutionBoard;
        private static DecisionBuilder? _decisionBuilder;
        private static Solver? _solver;
        private static IntVar[,]? _possibleBoard;

        public static int[,] SolveBoardMin(int[,] board)
        {
            SetUpConstrains(board);

            // Search for a solution
            _decisionBuilder = _solver!.MakePhase(_possibleBoard.Flatten(), Solver.INT_VAR_SIMPLE, Solver.ASSIGN_MIN_VALUE);

            if (SearchSolution())
            {
                return _solutionBoard!;
            }

            DisposeResources();

            return _solutionBoard = new int[9, 9];
        }

        public static int[,] SolveBoardMax(int[,] board)
        {
            SetUpConstrains(board);

            // Search for a solution
            _decisionBuilder = _solver!.MakePhase(_possibleBoard.Flatten(), Solver.INT_VAR_SIMPLE, Solver.ASSIGN_MAX_VALUE);

            if (SearchSolution())
            {
                return _solutionBoard!;
            }

            DisposeResources();

            return _solutionBoard = new int[9, 9];
        }

        private static void SetUpConstrains(int[,] board)
        {
            _solver = new Solver("SudokuSolver");

            // Define the variables
            _possibleBoard = _solver.MakeIntVarMatrix(9, 9, 1, 9, "board");

            // Define the constraints for rows and columns
            for (int i = 0; i < 9; i++)
            {
                _solver!.Add((from j in Enumerable.Range(0, 9) select _possibleBoard![i, j]).ToArray().AllDifferent());
                _solver!.Add((from j in Enumerable.Range(0, 9) select _possibleBoard![j, i]).ToArray().AllDifferent());
            }

            // Define the constraints for the 3x3 boxes
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _solver!.Add((from x in Enumerable.Range(i * 3, 3) from y in Enumerable.Range(j * 3, 3) select _possibleBoard![x, y]).ToArray().AllDifferent());
                }
            }

            // Add the initial values as constraints
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != 0)
                    {
                        _solver!.Add(_possibleBoard![i, j] == board[i, j]);
                    }
                }
            }
        }

        private static bool SearchSolution()
        {
            _solver!.NewSearch(_decisionBuilder);

            if (_solver!.NextSolution())
            {
                _solutionBoard = new int[9, 9];

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        _solutionBoard[i, j] = (int)_possibleBoard![i, j].Value();
                    }
                }

                DisposeResources();

                return true;
            }

            return false;
        }

        private static void DisposeResources()
        {
            _decisionBuilder!.Dispose();
            _solver!.Dispose();
            _possibleBoard = null;
        }
    }
}
