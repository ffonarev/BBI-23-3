using System;
using System.IO;

class Task
{
    protected string _array;
    public Task(string array)
    {
        _array = array;
    }
}
class Task1 : Task
{
    public Task1(string array) : base(array) { }
    public static int CountWords(string array)
    {
        string[] words = array.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        int count = 0;

        foreach (string word in words)
        {
            if (word.Length == 1)
            {
                count++;
            }
        }

        return count;
    }
}

class Task2 : Task
{
    public Task2(string array) : base(array) { }
    public static bool CheckBrackets(string input)
    {
        int openCount = 0, closeCount = 0;
        int[] brackets = new int[3];

        foreach (char c in input)
        {
            if (c == '(')
            {
                openCount++;
                brackets[0]++;
            }
            else if (c == ')')
            {
                closeCount++;
                brackets[0]--;
            }
            else if (c == '[')
            {
                brackets[1]++;
            }
            else if (c == ']')
            {
                brackets[1]--;
            }
            else if (c == '{')
            {
                brackets[2]++;
            }
            else if (c == '}')
            {
                brackets[2]--;
            }
        }

        return openCount == closeCount &&
               brackets[0] == 0 &&
               brackets[1] == 0 &&
               brackets[2] == 0;
    }
}

class Task3
{
    public void CreateFolderAndFiles()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Test";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string filePath1 = path + @"\cw2_1.json";
        string filePath2 = path + @"\cw2_2.json";

        if (!File.Exists(filePath1))
        {
            File.Create(filePath1).Dispose();
        }

        if (!File.Exists(filePath2))
        {
            File.Create(filePath2).Dispose();
        }
    }
}
class Program
{
    static void Main()
    {
        string array1 = "В этом задании нужно подсчитать такие слова как я, в, выа, ып, кып, т, е. сж: т!";
        Console.WriteLine($"Текст задания 1: {array1}");
        Task1 task1 = new Task1(array1);
        int wordCount = Task1.CountWords(array1);

        string array2 = "А вот( пусть { пытается} определить [ правильно ли] это)";
        string array22 = "А вот( пусть { пытается} определить [ правильно ли] это))";
        Console.WriteLine($"Первый текст задания 2 (правильный): {array2}");
        Console.WriteLine($"Второй текст задания 2 (неправильный): {array22}");
        Task2 task2 = new Task2(array2);
        Task2 task22 = new Task2(array22);
        bool result = Task2.CheckBrackets(array2);
        bool result2 = Task2.CheckBrackets(array22);


        Console.WriteLine($"Колличество слов и пр. длиной в одну букву: {wordCount}");
        Console.WriteLine($"Правильно ли расставлены скобки в первом тексте? Ответ: {result}");
        Console.WriteLine($"Правильно ли расставлены скобки во втором тексте? Ответ: {result2}");

        Task3 creator = new Task3();
        creator.CreateFolderAndFiles();
    }
}