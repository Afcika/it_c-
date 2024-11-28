using System;
using System.IO;

class Calculator
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Calculator!");

       
        string filePath = @"C:\Users\User\Desktop\testclacl.txt";

      
        double num1 = GetValidNumber("Enter the first number: ");
        double num2 = GetValidNumber("Enter the second number: ");

       
        string operation;
        while (true)
        {
            Console.WriteLine("Select an operation (+, -, *, /): ");
            operation = Console.ReadLine();

            if (operation == "+" || operation == "-" || operation == "*" || operation == "/")
                break;

            Console.WriteLine("Invalid operation. Please choose +, -, *, or /.");
        }

      
        double result = 0;
        switch (operation)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 != 0)
                    result = num1 / num2;
                else
                {
                    Console.WriteLine("Division by zero is not allowed. Please re-enter the second number.");
                    num2 = GetValidNumber("Enter the second number: ");
                    result = num1 / num2;
                }
                break;
        }

        
        Console.WriteLine($"Result: {result}");

       
        SaveOperationToFile(filePath, num1, num2, operation, result);

      
        ManageFile(filePath);
    }

    static double GetValidNumber(string prompt)
    {
        double number;
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out number))
                return number;

            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    static void SaveOperationToFile(string filePath, double num1, double num2, string operation, double result)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine($"{DateTime.Now}: {num1} {operation} {num2} = {result}");
            }
            Console.WriteLine("Operation saved to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the operation: {ex.Message}");
        }
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
