using System;

namespace B17_Ex02_BullsEyeConsole
{
    public class Program
    {
        private const string k_Lose = "No more guesses allowed. You Lost.";
        private const string k_AskForNewGame = "Would you like to start a new game? (Y/N)";

        public static void Main()
        {
            bool keepPlaying = true;

            while (keepPlaying)
            {
                Manager gameManager = new Manager();
                gameManager.GameOn();
                keepPlaying = gameManager.KeepPlaying;

                if (gameManager.PlayerWins)
                {
                    string winSentence = string.Format("You guessed after {0} steps!", gameManager.CurrentRound);
                    Console.WriteLine(winSentence);
                }
                else if (!keepPlaying)
                {
                    Console.WriteLine("Bye Bye");
                    break;
                }
                else
                {
                    Console.WriteLine(k_Lose);
                }

                Console.WriteLine(k_AskForNewGame);
                char answer;
                while (!char.TryParse(Console.ReadLine(), out answer))
                {
                    Console.WriteLine("invalid input");
                    Console.WriteLine(k_AskForNewGame);
                }

                keepPlaying = char.ToUpper(answer) == 'Y';
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }
    }
}
