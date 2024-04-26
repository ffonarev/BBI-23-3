using System;
using System.Runtime.CompilerServices;

class Task
{
    private string _text;
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
}
class Task1 : Task
{

    private static void prepdel(string text)
    {
        for(int i  = 0; i < text.Length; i++)
        {
            if (text[i] == ',' || text[i] == '.' || text[i] == '!' || text[i] == '?' || text[i] == ';' || text[i] == ':' || text[i] == '-')
            {
                text[i] = ' ';
            }
        }
    }
}
class Task2 : Task
{
    

    private static void lastwords(string text)
    {
        string[] answer = {};
        string word = "";
        int k = 0;
        for (int i = 0; i < text.Length; i++) 
        {
            if (text[i] == '.' || text[i] == '!' || text[i] == '?')
            {
                for(int j = i - 1; j >= 0; j--)
                {
                    if (text[j] != ' ')
                    {
                        word += text[j];
                    }
                    else
                    {
                        j = -1;
                    }
                }
                answer[k] = word;
                k++;
            }
        }

        return answer;
    }
}

class Program
{
    public static void Main()
    {
        Task task = new Task1();
        task = "sdg sedf j goispg  jsijg is jgs je9g jsg";
    }
}