﻿using System;
using System.Collections.Generic;

namespace B17_Ex02_BullsEyeConsole
{
    public class Player
    {
        private bool m_QuiteGame = false;

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
                    System.Console.WriteLine("Please only use numbers (4-10)");
                } else
                {
                    System.Console.WriteLine("Number not in range (4-10)");
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
                System.Console.WriteLine("Please enter your next guess <A - H> or 'Q' to quite");
                string inputWord = Console.ReadLine().ToUpper();
                char currentInputLetter;
                int letterCounter = 0;
                char previousLetter = 'Q';
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
                    else if (currentInputLetter == previousLetter)
                    {
                        Console.WriteLine("Please use each letter only once");
                        validLetter = false;
                    }
                    else if (!inRange(currentInputLetter))
                    {
                        Console.WriteLine("Please use only letters A - H");
                        validLetter = false;
                    }
                    else
                    {
                        userGuess.Add(currentInputLetter);
                    }
                    previousLetter = currentInputLetter;
                }

                if (letterCounter != 4 || !validLetter)
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
            inRange = (i_Letter >= 'A') && (i_Letter <= 'H');

            return inRange;
        }
    }
}