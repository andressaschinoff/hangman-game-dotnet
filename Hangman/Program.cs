namespace Hangman
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal partial class Program
    {
        static string correctWord;
        static char[] letters;
        static Player player;

        private static void Main(string[] args)
        {
            try
            {
                StartGame();
                PlayGame();
                EndGame();
            }
            catch
            {
                Console.WriteLine("Oops, something went wrong...");
            }
        }

        private static void StartGame()
        {
            string[] words;
            try
            {
                words = File.ReadAllLines(@"C:\Users\andressa.schinoff\Documents\words.txt");
            }
            catch
            {
                words = new string[] { "tree", "pie", "cat", "one" };
            }

            Random random = new Random();
            correctWord = words[random.Next(0, words.Length)];

            letters = new char[correctWord.Length];
            for (int i = 0; i < correctWord.Length; i++)
                letters[i] = '*';

            AskForUserName();
        }

        private static void AskForUserName()
        {
            do
            {
                Console.WriteLine("Please, enter your name.");
                string input = Console.ReadLine();
                player = new Player(input)
                {
                    Score = 100
                };
            }
            while (player.Name.Length < 2);
        }

        private static void PlayGame()
        {
            do
            {
                Console.Clear();
                DisplayMaskedWord();

                char guessedLetter = AskForLetter();
                CheckLetter(guessedLetter);
            } while (correctWord != new string(letters));
            Console.Clear();
        }

        private static void DisplayMaskedWord()
        {
            foreach (char letter in letters)
                Console.Write(letter);
            Console.WriteLine();
        }

        private static char AskForLetter()
        {
            string input;
            do
            {
                Console.WriteLine("Enter a letter:");
                input = Console.ReadLine();
            } while (input.Length != 1 || player.GuessedLetters.Contains(input[0]));

            char inputLetter = input[0];
            player.GuessedLetters.Add(inputLetter);

            return inputLetter;
        }
        private static void CheckLetter(char guessedLetter)
        {
            if (correctWord.Contains(guessedLetter))
            {
                for (int i = 0; i < correctWord.Length; i++)
                {
                    if (guessedLetter == correctWord[i])
                    {
                        letters[i] = guessedLetter;
                    }
                }
            }
            else
            {
                player.Score -= player.Score / (correctWord.Length + 3);
            }
        }

        private static void EndGame()
        {
            Console.WriteLine("Congrats!");
            Console.WriteLine($"Thanks for playing {player.Name}!");
            Console.WriteLine($"Your guesses: {player.GuessedLetters.Count} Score: {player.Score}");
        }
    }
}
