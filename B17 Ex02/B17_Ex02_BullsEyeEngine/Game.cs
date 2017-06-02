using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02_BullsEyeEngine
{
    class Game
    {
        private List<char> m_WordToGuess;
        private const int k_NumberOfLettersInWord = 4;
        private const int k_MinNumberOfRounds = 4;
        private const int k_MaxNumberOfRounds = 10;
        private const char k_FirstLetterPossible = 'A';
        private const char k_LastLetterPossible = 'H';
        private const int k_MaxCharsInFeedback = 7;

        public int MinNumberOfGuesses
        {
            get
            {
                return k_MinNumberOfRounds;
            }
        }

        public int MaxNumberOfGuesses
        {
            get
            {
                return k_MaxNumberOfRounds;
            }
        }

        public void randomizeNewWord()
        {
            List<char> wordToReturn = new List<char>();
            Random random = new Random();

            for (int i = 0; i < k_NumberOfLettersInWord; i++)
            {
                int nextLetterAsNumber = random.Next(k_FirstLetterPossible, k_LastLetterPossible);
                if (!wordToReturn.Contains((char)nextLetterAsNumber))
                {
                    char nextLetter = (char)nextLetterAsNumber;
                    wordToReturn.Insert(i, nextLetter);
                }
                else
                {
                    i--;
                }

            }
            m_WordToGuess = wordToReturn;
        }

        public List<char> FeedbackForPlayerGuess(List<char> i_PlayersGuess)
        {
            int counterInPlace = 0;
            int counterMissplaced = 0;
            List<char> feedbackOnGuess = new List<char>();

            for (int i = 0; i < k_NumberOfLettersInWord; i++)
            {
                // first cell is correctness of letter only, second cell is correctness of position
                List<bool> correctnessAndPosition = letterChecker(i_PlayersGuess[i], i);

                if (correctnessAndPosition[0] && !correctnessAndPosition[1])
                {
                    counterMissplaced++;
                }

                if (correctnessAndPosition[0] && correctnessAndPosition[1])
                {
                    counterInPlace++;
                }
            }


            for (int i = 0; i < counterInPlace; i++)
            {
                feedbackOnGuess.Add('V');
            }

            for (int i = 0; i < counterMissplaced; i++)
            {
                feedbackOnGuess.Add('X');
            }

            for (int i = counterInPlace + counterMissplaced; i < k_NumberOfLettersInWord; i++)
            {
                feedbackOnGuess.Add(' ');
            }

            return feedbackOnGuess;
        }

        private List<bool> letterChecker(char letterToCheck, int indexOfLetter)
        {
            List<bool> correctnessAndPosition = new List<bool>(2);
            correctnessAndPosition.Add(false);
            correctnessAndPosition.Add(false);
            bool letterNotFound = true;

            for (int i = 0; i < k_NumberOfLettersInWord && letterNotFound; i++)
            {
                if (letterToCheck == m_WordToGuess[i])
                {
                    correctnessAndPosition[0] = true;
                    if (indexOfLetter == i)
                    {
                        correctnessAndPosition[1] = true;
                    }
                    letterNotFound = false;
                }
            }
            return correctnessAndPosition;
        }
    }
}
