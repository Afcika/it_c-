using System;
using System.IO;

class Hangman
{
    static void Main(string[] args)
    {
        // Specify the file path for saving game data
        string filePath = @"C:\Users\User\Desktop\testhangman.txt";

        string[] words = { "saxli", "csharp", "fuli", "tamashi" };
        Random rand = new Random();
        string chosenWord = words[rand.Next(words.Length)];
        char[] guessedWord = new string('_', chosenWord.Length).ToCharArray();
        int attemptsLeft = 6;
        bool wordGuessed = false;

        Console.WriteLine("Welcome to Hangman!");
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine($"Game Start: {DateTime.Now}");
            writer.WriteLine($"Chosen Word: {chosenWord}");
        }

        while (attemptsLeft > 0 && !wordGuessed)
        {
            Console.WriteLine($"\nWord: {new string(guessedWord)}");
            Console.Write($"Attempts Left: {attemptsLeft}. Enter a letter: ");
            char guess = Console.ReadKey().KeyChar;
            Console.WriteLine();

            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine($"Player Guess: {guess}");
            }

            if (chosenWord.Contains(guess))
            {
                for (int i = 0; i < chosenWord.Length; i++)
                {
                    if (chosenWord[i] == guess)
                        guessedWord[i] = guess;
                }
            }
            else
            {
                attemptsLeft--;
            }

            if (new string(guessedWord) == chosenWord)
            {
                wordGuessed = true;
                Console.WriteLine($"Congratulations! You guessed the word: {chosenWord}");

                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine("Outcome: Win");
                }
            }
        }

        if (!wordGuessed)
        {
            Console.WriteLine($"Game Over! The word was: {chosenWord}");

            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine("Outcome: Lose");
            }
        }

        // View saved file content option
        ManageFile(filePath);
    }

    static void ManageFile(string filePath)
    {
        Console.WriteLine("\nFile Operations:");
        Console.WriteLine("1. View file content");
        Console.WriteLine("2. Delete the file");
        Console.WriteLine("3. Exit");

        while (true)
        {
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (File.Exists(filePath))
                    {
                        Console.WriteLine("\nFile Content:");
                        Console.WriteLine(File.ReadAllText(filePath));
                    }
                    else
                    {
                        Console.WriteLine("File does not exist.");
                    }
                    break;

                case "2":
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        Console.WriteLine("File deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("File does not exist.");
                    }
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
