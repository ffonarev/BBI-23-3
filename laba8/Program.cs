using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;




namespace lab8
{
    abstract class Task
    {
        public Task(string text) { }
        protected virtual string ParseText(string text) { return text; }
    }
    class Task_2 : Task
    {
        private string text;
        public Task_2(string text) : base(text) { this.text = text; }

        public string Encrypt(string text)
        {
            char[] punctuation = { ',', '.', '!', '?', ';', ':', '(', ')' };
            StringBuilder reversed = new StringBuilder();
            string[] words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                int PunctuationIndex = -1;

                foreach (char p in punctuation)
                {
                    if (word.Contains(p))
                    {
                        PunctuationIndex = word.IndexOf(p);
                        break;
                    }
                }
                if (PunctuationIndex != -1)
                {
                    string ReversedWord = new string(word.Substring(0, PunctuationIndex).ToCharArray().Reverse().ToArray());
                    reversed.Append(ReversedWord + word[PunctuationIndex]);
                }
                else
                {
                    string ReversedWord = new string(word.ToCharArray().Reverse().ToArray());
                    reversed.Append(ReversedWord);
                }
                if (i < words.Length - 1)
                {
                    reversed.Append(" ");
                }
            }
            return (reversed).ToString();
        }
        public string Decrypt(string text2)
        {
            text = Encrypt(text2);
            return text;
        }
    }

    class Task_4 : Task
    {
        private string text;
        public Task_4(string text) : base(text) { this.text = text; }

        public int TextLevel(string text4)
        {
            int TextLevel = 0;
            string[] words = text.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                foreach (char c in word)
                {
                    if (char.IsPunctuation(c)) { TextLevel++; }
                }
            }
            TextLevel += words.Length;
            return TextLevel;
        }
    }

    class Task_6 : Task
    {
        private string text;
        public Task_6(string text) : base(text) { this.text = text; }

        public int[] CountSyllables(string text)
        {
            int one = 0;
            int two = 0;
            int three = 0;
            int more = 0;
            int counter = 0;
            string[] words = text.Split(' ');
            Regex regex = new Regex("[ёуеыаоэяиюyeuioa]", RegexOptions.IgnoreCase);
            foreach (string word in words)
            {
                counter = regex.Matches(word).Count;
                if (counter == 1) { one++; }
                else if (counter == 2) { two++; }
                else if (counter == 3) { three++; }
                else { more++; }
            }
            int[] count = new int[] { one, two, three, more };
            return count;
        }
    }

    class Task_8 : Task
    {
        public List<string> lines = new List<string>();
        private string text;
        public Task_8(string text) : base(text) { this.text = text; }

        public string ParseText(string text)
        {
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder line = new StringBuilder();
            int LineLength = 0;

            foreach (string word in words)
            {
                if (LineLength + word.Length + 1 > 50)
                {
                    Adjustline(line, 50);
                    lines.Add(line.ToString());
                    line.Clear();
                    LineLength = 0;
                }
                if (line.Length > 0)
                {
                    line.Append(" ");
                    LineLength++;
                }
                line.Append(word);
                LineLength += word.Length;
            }
            if (line.Length > 0)
            {
                Adjustline(line, 50);
                lines.Add(line.ToString());
            }

            return string.Join("\n", lines);
        }

        private void Adjustline(StringBuilder line, int neededLength)
        {
            while (line.Length < neededLength)
            {
                for (int i = 0; i < line.Length && line.Length < neededLength; i++)
                {
                    if (line[i] == ' ' && (i == 0 || line[i - 1] != ' ')) { line.Insert(i, ' '); i++; }
                }
            }
        }
    }

    struct LetterPair
    {
        public string _pair { get; }
        public int count { get; private set; }
        public string _code { get; set; }


        public LetterPair(string pair)
        {
            _pair = pair;
            count = 1;
            _code = "...";
        }

        public void Increment()
        {
            count++;
        }

        public static string[] CreateCode(string text)
        {
            string[] code = new string[10];
            int c = 0;
            for (int i = 33; i < 127; i++)
            {
                if (text.Contains((char)i) == false)
                {
                    code[c] = char.ToUpper((char)i).ToString();
                    c++;
                    if (c == 10) { break; }
                }
            }
            return code;
        }
    }

    class Task_9 : Task
    {
        private string text;
        private string[] Keys;
        private string[] Codes;
        private List<LetterPair> letterPairs;
        public Task_9(string text) : base(text) 
        { 
            this.text = text;
            this.letterPairs = new List<LetterPair>();
        }
        public string[] GetKeys()
        {
            return Keys;
        }
        public string[] GetCodes()
        {
            return Codes;
        }
        public override string ToString()
        {
            return ParseText(text);
        }
        private List<LetterPair> CreateLetterPairRu()
        {
            List<LetterPair> pairs = new List<LetterPair>();
            for (int i = 1072; i < 1105; i++)
            {
                for (int j = 1072; j < 1105; j++)
                {
                    int n1 = i;
                    int n2 = j;
                    if (i == 1104) { n1 += 1; }
                    if (j == 1104) { n2 += 1; }
                    char first = (char)n1;
                    char second = (char)n2;
                    string k = first.ToString() + second.ToString();
                    pairs.Add(new LetterPair(k));
                }
            }
            return pairs;
        }
        private List<LetterPair> CreateLetterPairEng()
        {
            List<LetterPair> pairs = new List<LetterPair>();
            for (int i = 97; i < 123; i++)
            {
                for (int j = 97; j < 123; j++)
                {
                    int n1 = i;
                    int n2 = j;
                    char first = (char)n1;
                    char second = (char)n2;
                    string k = first.ToString() + second.ToString();
                    pairs.Add(new LetterPair(k));
                }
            }
            return pairs;
        }
        protected bool CheckTheLanguage()
        {
            string rusLetters = "ёЁйЙцЦуУкКеЕнНгГшШщЩзЗхХъЪфФыЫвВаАпПрРоОлЛдДжЖэЭяЯчЧсСмМиИтТьЬбБюЮ";
            for (int i = 0; i < rusLetters.Length; i++)
            {
                if (text.Contains(rusLetters[i])) { return true; }
            }
            return false;
        }


        protected override string ParseText(string text)
        {
            List<LetterPair> letterPairs;
            if (CheckTheLanguage() == false) { letterPairs = CreateLetterPairEng(); }
            else { letterPairs = CreateLetterPairRu(); }

            for (int i = 0; i < text.Length - 1; i++)
            {
                string pair = text[i].ToString() + text[i + 1].ToString();
                var letterPair = letterPairs.FirstOrDefault(lp => lp._pair == pair);
                if(letterPair._pair != null) { letterPair.Increment(); }
            }

            var sortedLetterPairs = letterPairs.OrderByDescending(lp => lp.count).Take(10).ToArray();


            //for (int i = 0; i < keys.Length; i++)
            //{
            //    text = text.Replace(keys[i], codes[i]);
            //}
            //Keys = keys;
            //Codes = codes;

            return text;
        }
    }

    class Task_10 : Task
    {
        string[] codes;
        string[] keys;
        string text;
        public Task_10(string text, string[] codes, string[] keys) : base(text)
        {
            this.text = text;
            this.codes = codes;
            this.keys = keys;
        }
        public override string ToString()
        {
            return ParseText(text);
        }
        protected override string ParseText(string text)
        {
            string[] ke = keys;
            string[] code = codes;

            for (int i = 0; i < ke.Length; i++)
            {
                text = text.Replace(code[i], ke[i]);
            }

            return text;
        }
    }


    class Program
    {
        public static void Main()
        {
            string text = "Я не понимаю, я не понимаю! Просто растворяюсь, я седьмой день?";
            Task_2 task2 = new Task_2(text);
            Console.WriteLine("Задание 2:");
            Console.WriteLine("Текст: ");
            Console.WriteLine(text);
            Console.WriteLine("\nТекст зашифрован:");
            Console.WriteLine(task2.Encrypt(text));
            Console.WriteLine("Текст расшифрован:");
            Console.WriteLine(task2.Decrypt(task2.Encrypt(text)));

            Task_4 task4 = new Task_4(text);
            Console.WriteLine("\nЗадание 4:");
            Console.WriteLine(task4.TextLevel(text));

            Task_6 task6 = new Task_6(text);
            int[] count = task6.CountSyllables(text);
            Console.WriteLine("\nЗадание 6:");
            for (int i = 0; i < count.Length; i++)
            {
                Console.WriteLine(" количество слоговов: {0} \t количество слов : {1} ", i + 1, count[i]);
            }

            Console.WriteLine("\nЗадание 8:");
            Task_8 task8 = new Task_8(text);
            Console.WriteLine(task8.ParseText(text));

            Console.WriteLine("\nЗадание 9:");
            Task_9 task9 = new Task_9(text);
            Console.WriteLine(task9);

            Console.WriteLine("\nЗадание 10:");
            Task_10 task10 = new Task_10(task9.ToString(), task9.GetCodes(), task9.GetKeys());
            Console.WriteLine(task10);
        }
    }
}
