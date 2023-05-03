using Google.OrTools.ConstraintSolver;
using SudokuGame.Models;
using SudokuGame.Utilities;
using Solver = Google.OrTools.ConstraintSolver.Solver;

namespace SudokuGame.Solvers
{
    public class ConstraintProg
    {
        private static int[,]? _solutionBoard = null!;
        private static DecisionBuilder? _decisionBuilder = null!;
        private static Solver? _solver = null!;
        private static IntVar[,]? _possibleBoard = null!;
        private static SolutionCollector? _solutionCollector = null!;
        private static int boardSize;
        private static int internalBlockSize;

        public static bool HasMultipleSolutions(int[,] board)
        {
            boardSize = Settings.BOARD_SIZE;
            internalBlockSize = BoardUtil.InternalBlockSize(boardSize);

            SetUpConstrains(board);

            _solutionCollector = _solver!.MakeAllSolutionCollector();
            _solutionCollector.Add(_possibleBoard.Flatten());

            _solver.NewSearch(_decisionBuilder, _solutionCollector);

            while (_solver.NextSolution())
            {
                if (_solutionCollector.SolutionCount() > 1)
                {
                    DisposeResources();

                    return false;
                }
            }

            DisposeResources();

            return true;
        }

        public static int[,] SolveBoard(int[,] board)
        {
            SetUpConstrains(board);

            if (SearchSolution())
            { 
                return _solutionBoard!;
            }

            return _solutionBoard = new int[1,1];
        }

        private static void SetUpConstrains(int[,] board)
        {
            _solver = new Solver("SudokuSolver");

            // Define the variables
            _possibleBoard = _solver.MakeIntVarMatrix(boardSize, boardSize, 1, boardSize, "board");

            // Define the constraints for rows and columns
            for (int i = 0; i < boardSize; i++)
            {
                _solver!.Add((from j in Enumerable.Range(0, boardSize) select _possibleBoard![i, j]).ToArray().AllDifferent());
                _solver!.Add((from j in Enumerable.Range(0, boardSize) select _possibleBoard![j, i]).ToArray().AllDifferent());
            }

            // Define the constraints for the 3x3 boxes
            for (int i = 0; i < internalBlockSize; i++)
            {
                for (int j = 0; j < internalBlockSize; j++)
                {
                    _solver!.Add((from x in Enumerable.Range(i * internalBlockSize, internalBlockSize) from y in Enumerable.Range(j * internalBlockSize, internalBlockSize) select _possibleBoard![x, y]).ToArray().AllDifferent());
                }
            }

            // Add the initial values as constraints
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] != 0)
                    {
                        _solver!.Add(_possibleBoard![i, j] == board[i, j]);
                    }
                }
            }

            _decisionBuilder = _solver!.MakePhase(_possibleBoard.Flatten(), Solver.INT_VAR_SIMPLE, Solver.ASSIGN_MIN_VALUE);
        }

        private static bool SearchSolution()
        {
            boardSize = Settings.BOARD_SIZE;
            internalBlockSize = BoardUtil.InternalBlockSize(boardSize);

            _solver!.NewSearch(_decisionBuilder);

            if (_solver!.NextSolution())
            {
                _solutionBoard = new int[boardSize, boardSize];

                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        _solutionBoard[i, j] = (int)_possibleBoard![i, j].Value();
                    }
                }

                DisposeResources();

                return true;
            }

            DisposeResources();

            return false;
        }

        private static void DisposeResources()
        {
            _decisionBuilder!.Dispose();
            _solver!.Dispose();
            if (_solutionCollector != null) _solutionCollector!.Dispose();
            _possibleBoard = null;
        }
    }
}
 