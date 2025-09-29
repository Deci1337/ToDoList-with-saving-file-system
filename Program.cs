using System;
using System.IO;

namespace ToDoList
{
    internal class Program
    {
        static void Main()
        {
            string file = "test.txt";

            string directory = ChoosingPath();
          
            if (directory == "Exit")
                Environment.Exit(0);
          
            if (CheckPath(directory))
            {
                if (File.ReadAllText($@"{directory}\{file}") == "")
                    File.WriteAllText($@"{directory}\{file}", $@"{directory}", System.Text.Encoding.UTF8); // creating file by directory
            }
            else
            {
                Console.WriteLine("Error path");
                Environment.Exit(0);
            }
          
            while (true)
            {
                // For breaking input just close the program. I'll fix that...
                ShowInfo(directory, file);
                AddNote(Console.ReadLine(), directory, file);
                Console.Clear();
            }

        }
        static string EnterFilePath()
        {
            bool input = true;
            string currentDirectory = @"C:";
            string path = "";
            
            Console.SetCursorPosition(0, 1);
            Console.Write("Input your path. To apply press Enter");
            Console.SetCursorPosition(0, 0);

            while (input)
            {
                Console.Write(currentDirectory + @"\");
              
                path = Console.ReadLine();
                if (path != "")
                {
                    if (path[1] == ':')
                        currentDirectory = path;
                    else
                        currentDirectory += @"\" + path;
                }
                else
                {
                    Console.Clear();
                    Console.Write(currentDirectory);
                    input = false;
                    break;
                }
                Console.Clear();
            }
            LatestPath(currentDirectory);
            return currentDirectory;
        }

        static bool CheckPath(string directory) => 
            (Directory.Exists(directory));

        static void AddNote(string note, string directory, string file) =>
            File.WriteAllText($@"{directory}\{file}", File.ReadAllText($@"{directory}\{file}") + "\n" + note);

        static void ShowInfo(string directory, string file) =>
            Console.WriteLine(File.ReadAllText($@"{directory}\{file}"));

        static void LatestPath(string directory) => File.WriteAllText("config.txt", directory);

        static string ChoosingPath()
        {
            Console.WriteLine("Choose an action:\n1 - Use the latest path\n2 - Enter a new path\n3 - Exit");
          
            ConsoleKey key = Console.ReadKey().Key;
          
            Console.Clear();
          
            switch (key)
            {
                case ConsoleKey.D1:
                    return (File.ReadAllText("config.txt") != "") ? File.ReadAllText("config.txt") : "No latest path";

                case ConsoleKey.D2:
                    return EnterFilePath();

                case ConsoleKey.D3:
                    return "Exit";
                
                default: 
                        return ChoosingPath();
            }
        }
    }
}
