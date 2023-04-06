using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SudokuGame.Features
{
    public class EndGame
    {
        private static Timer? _timer;

        public static void SetTimer(int minutes)
        {
            _timer = new Timer(5000);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnTimedEvent!;
            _timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _timer!.Stop();

            DrawGame.Timeout();

            _timer!.Dispose();
            
        }

        public static void GameOver()
        {
            string gameOver = @"
             ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ██╗   ██╗███████╗██████╗ 
            ██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔══██╗
            ██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║██║   ██║█████╗  ██████╔╝
            ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗
            ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║
             ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝";

            Console.Clear();

            Console.WriteLine(gameOver);
            Console.WriteLine("Press enter to continue..");
            Console.ReadLine();
        }
    }
}
