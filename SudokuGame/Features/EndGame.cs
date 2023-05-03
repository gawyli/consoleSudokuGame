using SudokuGame.Models;
using SudokuGame.Strategy;
using System.Timers;
using WindowsInput;
using WindowsInput.Native;
using Timer = System.Timers.Timer;

namespace SudokuGame.Features
{
    public class EndGame
    {
        private static Timer? _timer;

        public static void SetTimer(int minutes)
        {
            _timer = new Timer(minutes);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnTimedEvent!;
            _timer.Enabled = true;
        }

        public static void Over(bool hasWon, int score)
        {
            if (hasWon)
            {
                GameWin(score);
            }
            else
            {
                GameOver(score);
            }
        }

        public static void GameOver(int score)
        {
            string gameOver = @"
             ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ██╗   ██╗███████╗██████╗ 
            ██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔══██╗
            ██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║██║   ██║█████╗  ██████╔╝
            ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗
            ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║
             ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝";


            Blinker(gameOver);

            Console.WriteLine($"\nYour score: {score}");
            Console.WriteLine("Press enter to continue..");
            Console.ReadLine();
        }

        public static void GameWin(int score)
        {
            string gameWon = @"                                               
             /$$      /$$ /$$                                         /$$
            | $$  /$ | $$|__/                                        | $$
            | $$ /$$$| $$ /$$ /$$$$$$$  /$$$$$$$   /$$$$$$   /$$$$$$ | $$
            | $$/$$ $$ $$| $$| $$__  $$| $$__  $$ /$$__  $$ /$$__  $$| $$
            | $$$$_  $$$$| $$| $$  \ $$| $$  \ $$| $$$$$$$$| $$  \__/|__/
            | $$$/ \  $$$| $$| $$  | $$| $$  | $$| $$_____/| $$          
            | $$/   \  $$| $$| $$  | $$| $$  | $$|  $$$$$$$| $$       /$$
            |__/     \__/|__/|__/  |__/|__/  |__/ \_______/|__/      |__/";


            Blinker(gameWon);

            Console.WriteLine($"\nYour score: {score}");
            Console.WriteLine("Press enter to continue..");
            Console.ReadLine();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _timer!.Stop();

            Timeout();

            _timer!.Dispose();

        }

        private static void Timeout()
        {
            var sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            DrawGameStrategy.SetOver();
        }

        private static void Blinker(string text)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();

                Task.Delay(50).Wait();

                Console.WriteLine(text);

                Task.Delay(100).Wait();
            }
        }
    }
}
