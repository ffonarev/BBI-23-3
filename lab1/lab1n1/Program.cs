using System;

namespace lab1n1
{
    struct Jumps
    {
        private string _surname;
        private string _society;
        private double _res1;
        private double _res2;
        private double _totalres;
        public Jumps(string surname, string society, double res1, double res2)
        {
            _surname = surname;
            _society = society;
            _res1 = res1;
            _res2 = res2;
            _totalres = res1 + res2;
        }
        public double totalres => _totalres;
        public void Print()
        {
            Console.WriteLine("{0,-7} | {1,-3} | {2,-3} | {3, -3} | {4, -3}", _surname, _society, _res1, _res2, _totalres);
        }
        static public void ShellSort(Jumps[] contestants)
        {
            
            int d = contestants.Length / 2;
            while (d >= 1)
            {
                for (int i = d; i < contestants.Length; i++)
                {
                    int j = i;
                    Jumps temp = contestants[i];
                    double x = contestants[i].totalres;
                    while ((j >= d) && (contestants[j - d].totalres > x))
                    {
                        contestants[j] = contestants[j - d];
                        j = j - d;
                    }
                    contestants[j] = temp;

                }
                d /= 2;
            }
        }

    }
    internal class Program
    {

       
        static void Main(string[] args)
        {
            Jumps[] contestants = new Jumps[5]
            {
                new Jumps("Ivanov", "Team A", 4, 5),
                new Jumps("Petrov", "Team A", 2.5, 7),
                new Jumps("White", "Team B", 4, 6),
                new Jumps("Black", "Team B", 4, 7),
                new Jumps("Grey", "Team B", 4, 1)
            };

            Console.WriteLine("Список участников");
            Console.WriteLine("Имя      Команда  Рез 1  Рез 2");
            for (int i = 0; i < contestants.Length; i++)
            {
                contestants[i].Print();
            }
            Jumps.ShellSort(contestants);
            Console.WriteLine("Сортированный список");
            for (int i = 0; i < contestants.Length; i++)
            {
                contestants[i].Print();
            }
        }
    }
}
