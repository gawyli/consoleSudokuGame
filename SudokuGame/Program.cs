using SudokuGame.Features;
using SudokuGame.Models;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;


Player player = new Player();
Menu menu = new Menu();


string prompt = "Select:";
string[] options = { "Start game", "Load game", "Ranking", "Exit game" };

int selectedIndex = menu.Run(prompt, options);
SelectedOption(selectedIndex);



void SelectedOption(int selectedIndex)
{
    string[] timeOptions = { "None", "10min", "15min", "20min", "25min" };
    string[] levelOptions = { "Easy", "Medium", "Hard" };

    
    switch (selectedIndex)
	{
		case 0:
            int timeIndex = menu.Run("Select time constraints:", timeOptions);
            int levelIndex = menu.Run("Select level:", levelOptions);
            Console.WriteLine("Enter your username: ");
            player.Username = Console.ReadLine()!;
            StartTheGame(timeIndex, levelIndex);
			break;
        case 1:
            LoadGame();
            break;
        case 2:
            Ranking();
            break;
        case 3:
            Environment.Exit(0);
            break;
	}
}

void Ranking()
{
    Console.WriteLine($"Ranking:");
    Console.ReadKey(true);
}

void LoadGame()
{
    Console.WriteLine($"Game loaded!");
    Console.ReadKey(true);
}

void StartTheGame(int timeIndex, int levelIndex)
{
    Console.WriteLine($"Game started..{timeIndex}, {levelIndex}, {player.Username}!");
    Console.ReadKey(true);
}