using System;
using System.Collections.Generic;

namespace B17_Ex02_BullsEyeConsole
{
    public class Player
    {
        private bool m_QuiteGame = false;
        private readonly int k_NumberOfLettersInWord = 4;
        private readonly int k_MinNumberOfRounds = 4;
        private readonly int k_MaxNumberOfRounds = 10;
        private readonly char k_FirstLetterPossible = 'A';
        private readonly char k_LastLetterPossible = 'H';

        public bool QuiteGame
        {
            get
            {
                return m_QuiteGame;
            }
        }

        public int ChooseNumberOfGuesses(int i_MinNumberOfGuesses, int i_MaxNumberOfGuesses)
        {
            System.Console.WriteLine("Please enter number of guesses...");
            int numberOfGuesses;
            string userInput = Console.ReadLine();
            bool isNumber = int.TryParse(userInput, out numberOfGuesses);

            while (!isNumber || (numberOfGuesses < i_MinNumberOfGuesses)
                   || (numberOfGuesses > i_MaxNumberOfGuesses))
            {
                if (!isNumber)
                {
                    System.Console.WriteLine(string.Format("Please only use numbers ({0}-{1})", 
                                                        k_MinNumberOfRounds, k_MaxNumberOfRounds));
                } else
                {
                    System.Console.WriteLine(string.Format("Number not in range ({0}-{1})", 
                                                        k_MinNumberOfRounds, k_MaxNumberOfRounds));
                }
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out numberOfGuesses);
            }

            return numberOfGuesses;
        }

        public List<char> GuessWord()
        {
            List<char> userGuess = new List<char>();
            bool validGuess = false;

            while (!validGuess)
            {
                System.Console.WriteLine(string.Format("Please enter your next guess <{0} - {1}> or 'Q' to quite", 
                                                        k_FirstLetterPossible, k_LastLetterPossible));
                string inputWord = Console.ReadLine().ToUpper();
                char currentInputLetter;
                int letterCounter = 0;
                bool validLetter = true;

                for (int i = 0; i < inputWord.Length && validLetter; i += 2)
                {
                    currentInputLetter = inputWord[i];
                    letterCounter++;
                    if (currentInputLetter == 'Q')
                    {
                        m_QuiteGame = true;
                        return null;
                    }
                    else if (userGuess.Contains(currentInputLetter))
                    {
                        Console.WriteLine("Please use each letter only once");
                        validLetter = false;
                    }
                    else if (!inRange(currentInputLetter))
                    {
                        Console.WriteLine(string.Format("Please use only letters {0} - {1}",
                                                k_FirstLetterPossible, k_LastLetterPossible));
                        validLetter = false;
                    }
                    else
                    {
                        userGuess.Add(currentInputLetter);
                    }
                }

                if (letterCounter != k_NumberOfLettersInWord || !validLetter)
                {
                    validGuess = false;
                    userGuess.Clear();
                }
                else
                {
                    validGuess = true;
                }
            }

            return userGuess;
        }

        private bool inRange(char i_Letter)
        {
            bool inRange = false;
            inRange = (i_Letter >= k_FirstLetterPossible) && (i_Letter <= k_LastLetterPossible);

            return inRange;
        }
    }
}
