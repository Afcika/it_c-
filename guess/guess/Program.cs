using System;
using System.IO;

class GuessTheNumber
{
    static void Main(string[] args)
    {
      
        string filePath = @"C:\Users\User\Desktop\testguess.txt";

        Random rand = new Random();
        int targetNumber = rand.Next(1, 101);
        int attempts = 0;
        bool guessedCorrectly = false;

        Console.WriteLine("Guess the Number (between 1 and 100)");

        
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine($"Game Start: {DateTime.Now}");
            writer.WriteLine($"Target Number: {targetNumber}");
        }

        while (!guessedCorrectly)
        {
            Console.Write("Enter your guess: ");
            if (!int.TryParse(Console.ReadLine(), out int guess))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            attempts++;
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine($"Player Guess: {guess}");
            }

            if (guess < targetNumber)
                Console.WriteLine("Higher!");
            else if (guess > targetNumber)
                Console.WriteLine("Lower!");
            else
            {
                Console.WriteLine($"Correct! You guessed it in {attempts} attempts.");
                guessedCorrectly = true;

               
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine($"Outcome: Win in {attempts} attempts");
                }
            }
        }

      
        ManageFile(filePath);
    }

    static void ManageFile(string filePath)
    {
        Console.WriteLine("\nFile Operations:");
        Console.WriteLine("1. View file content");
        Console.WriteLine("2. Exit");

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
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
