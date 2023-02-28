using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SudokuGame.Navigation
{
    public class MenuLogic
    {
        const string TITLE = @"
             __              _         _                 ___                         
            / _\  _   _   __| |  ___  | | __ _   _      / _ \  __ _  _ __ ___    ___ 
            \ \  | | | | / _` | / _ \ | |/ /| | | |    / /_\/ / _` || '_ ` _ \  / _ \
            _\ \ | |_| || (_| || (_) ||   < | |_| |   / /_\\ | (_| || | | | | ||  __/
            \__/  \__,_| \__,_| \___/ |_|\_\ \__,_|   \____/  \__,_||_| |_| |_| \___|
                                                        ";
        private static int SelectedIndex { get; set; } = 0;

        public MenuLogic()
        {
        }

        private static void DisplayOptions(string prompt, string[] options)
        {
            WriteLine(TITLE);
            WriteLine(prompt);

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "->";
                    ForegroundColor = ConsoleColor.Magenta;
                    BackgroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.Yellow;
                    BackgroundColor = ConsoleColor.Magenta;
                }

                WriteLine($"{prefix} << {currentOption} >>");

            }
            ResetColor();
        }

        public static int RunMenu(string prompt, string[] options)
        {
            SelectedIndex = 0;
            ConsoleKey keyPressed;

            do
            {
                Clear();
                DisplayOptions(prompt, options);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }


            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}


