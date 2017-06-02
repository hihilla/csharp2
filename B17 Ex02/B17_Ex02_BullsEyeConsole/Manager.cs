using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02_BullsEyeConsole
{
    public class Manager
    {
        private int m_PlayersNumberOfRounds;
        private int m_CurrentRound;
        private List<List<char>> m_ListOfPlayerGuesses = new List<List<char>>();
        private List<List<char>> m_ListOfGuessesFeedback = new List<List<char>>();
        private StringBuilder m_AllResaults = new StringBuilder();
        private bool m_PlayerWins = false;
        private bool m_KeepPlaying = true;
        private const string k_boardStatus = "Current board status:";
        private const string k_TopRow =    "|Pins:    |Results:|";
        private const string k_Delimiter = "|=========|========|";
        private const string k_EmptyRow =  "|         |        |";
        private const string k_FirstRow =  "| # # # # |        |";

        public bool KeepPlaying
        {
            get
            {
                return m_KeepPlaying;
            }
        }
        public bool PlayerWins
        {
            get
            {
                return m_PlayerWins;
            }
        }
        public int CurrentRound
        {
            get
            {
                return m_CurrentRound;
            }
        }

        public void GameOn()
        {
            B17_Ex02_BullsEyeEngine.Game game = new B17_Ex02_BullsEyeEngine.Game();
            game.randomizeNewWord();
            Player player = new Player();

            m_PlayersNumberOfRounds = player.ChooseNumberOfGuesses(game.MinNumberOfGuesses,
                                                                   game.MaxNumberOfGuesses);
            PrintBoard();
            for (m_CurrentRound = 0; m_CurrentRound < m_PlayersNumberOfRounds && !player.QuiteGame && !m_PlayerWins; m_CurrentRound++)
            {
                List<char> userGuess = player.GuessWord();
                if (player.QuiteGame)
                {
                    m_KeepPlaying = false;
                    m_CurrentRound--;
                    return;
                }
                List<char> guessFeedback = game.FeedbackForPlayerGuess(userGuess);
                if (isWin(guessFeedback))
                {
                    m_PlayerWins = true;
                }
                m_ListOfPlayerGuesses.Add(userGuess);
                m_ListOfGuessesFeedback.Add(guessFeedback);
                addRoundToResults(userGuess, guessFeedback);
                PrintBoard();
            }
        }

        public void PrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(k_boardStatus);
            Console.WriteLine();
            Console.WriteLine(k_TopRow);
            Console.WriteLine(k_Delimiter);
            Console.WriteLine(k_FirstRow);
            Console.WriteLine(k_Delimiter);
            Console.Write(m_AllResaults.ToString());
            for (int i = m_CurrentRound; i < m_PlayersNumberOfRounds; i++)
            {
                Console.WriteLine(k_EmptyRow);
                Console.WriteLine(k_Delimiter);
            }
        }

        private void addRoundToResults(List<char> i_Guess, List<char> i_Feedback)
        {
            string guess = string.Format("| {0} {1} {2} {3} |",
                                         i_Guess[0], i_Guess[1],
                                         i_Guess[2], i_Guess[3]);
            string feedback = string.Format("{0} {1} {2} {3} |\n",
                                            i_Feedback[0], i_Feedback[1],
                                            i_Feedback[2], i_Feedback[3]);
            string result = guess + feedback + k_Delimiter + "\n";
            m_AllResaults.Append(result);

        }

        private bool isWin(List<char> i_Feedback)
        {
            bool isWin = true;
            foreach (char letter in i_Feedback)
            {
                isWin = isWin && (letter == 'V');
            }
            return isWin;
        }

    }
}
