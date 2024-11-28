using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentManagementSystem
{
    class Student
    {
        public string Name { get; private set; }
        public int RollNumber { get; private set; }
        // inkapsulaciisas 
        public char Grade { get; private set; }

        public Student(string name, int rollNumber, char grade)
        {
            Name = name;
            RollNumber = rollNumber;
            Grade = grade;
        }

        public void UpdateGrade(char newGrade)
        {
            Grade = newGrade;
        }

        public void DisplayStudentDetails()
        {
            Console.WriteLine($"Name: {Name}, Roll Number: {RollNumber}, Grade: {Grade}");
        }

        public string ToFileFormat()
        {
            return $"{Name},{RollNumber},{Grade}";
        }

        public static Student FromFileFormat(string line)
        {
            var parts = line.Split(',');
            return new Student(parts[0], int.Parse(parts[1]), char.Parse(parts[2]));
        }
    }

    class Program
    {
        private static List<Student> students = new List<Student>();
        private const string filePath = @"C:\Users\User\Desktop\ttttt.txt";

        static void Main(string[] args)
        {
            LoadStudentsFromFile();

            while (true)
            {
                Console.WriteLine("\n--- Student Management System ---");
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search Student by Roll Number");
                Console.WriteLine("4. Update Student Grade");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        ViewAllStudents();
                        break;
                    case 3:
                        SearchStudent();
                        break;
                    case 4:
                        UpdateStudentGrade();
                        break;
                    case 5:
                        Console.WriteLine("Exiting the system. Goodbye!");
                        SaveStudentsToFile();
                        return;
                }
            }
        }

        private static void LoadStudentsFromFile()
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    students.Add(Student.FromFileFormat(line));
                }
            }
        }

        private static void SaveStudentsToFile()
        {
            var lines = students.Select(s => s.ToFileFormat()).ToList();
            File.WriteAllLines(filePath, lines);
        }

        private static void AddStudent()
        {
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            Console.Write("Enter roll number: ");
            if (!int.TryParse(Console.ReadLine(), out int rollNumber))
            {
                Console.WriteLine("Invalid roll number. Please enter a valid integer.");
                return;
            }

            // Check if the roll number already exists
            if (students.Any(s => s.RollNumber == rollNumber))
            {
                Console.WriteLine("Roll number already in use. Please use a unique roll number.");
                return;
            }

            Console.Write("Enter grade (A-F): ");
            char grade = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (!"ABCDEF".Contains(char.ToUpper(grade)))
            {
                Console.WriteLine("Invalid grade. Please enter a grade between A and F.");
                return;
            }

            var student = new Student(name, rollNumber, char.ToUpper(grade));
            students.Add(student);
            Console.WriteLine("Student added successfully!");

            SaveStudentsToFile();
        }

        private static void ViewAllStudents()
        {
            Console.WriteLine("\n--- All Students ---");
            foreach (var student in students)
            {
                student.DisplayStudentDetails();
            }
        }

        private static void SearchStudent()
        {
            Console.Write("Enter roll number to search: ");
            if (!int.TryParse(Console.ReadLine(), out int rollNumber))
            {
                Console.WriteLine("Invalid roll number. Please enter a valid integer.");
                return;
            }

            var student = students.FirstOrDefault(s => s.RollNumber == rollNumber);
            if (student != null)
            {
                Console.WriteLine("Student found:");
                student.DisplayStudentDetails();
            }
            else
            {
                Console.WriteLine("No student found with the given roll number.");
            }
        }

        private static void UpdateStudentGrade()
        {
            Console.Write("Enter roll number to update grade: ");
            if (!int.TryParse(Console.ReadLine(), out int rollNumber))
            {
                Console.WriteLine("Invalid roll number. Please enter a valid integer.");
                return;
            }

            var student = students.FirstOrDefault(s => s.RollNumber == rollNumber);
            if (student == null)
            {
                Console.WriteLine("No student found with the given roll number.");
                return;
            }

            Console.Write("Enter new grade (A-F): ");
            char newGrade = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (!"ABCDEF".Contains(char.ToUpper(newGrade)))
            {
                Console.WriteLine("Invalid grade. Please enter a grade between A and F.");
                return;
            }

            student.UpdateGrade(char.ToUpper(newGrade));
            Console.WriteLine("Grade updated successfully!");

            SaveStudentsToFile();
        }
    }
}
