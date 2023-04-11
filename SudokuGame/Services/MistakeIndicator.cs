namespace SudokuGame.Services
{
    public class MistakeIndicator
    {
        public static bool Turn(bool isOn)
        {
            switch (isOn)
            {
                case true:
                    return false;
                case false:
                    return true;
            }
        }
    }
}
