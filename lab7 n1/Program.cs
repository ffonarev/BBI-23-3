﻿using System;

namespace lab1n1
{
    class Jumps
    {
        private string _surname;
        private string _society;
        private double _res1;
        private double _res2;
        private double _totalres;
        private bool _disqual;
        public Jumps(string surname, string society, double res1, double res2)
        {
            _surname = surname;
            _society = society;
            _res1 = res1;
            _res2 = res2;
            _totalres = res1 + res2;
            _disqual = false;
        }
        public void Disqualification()
        {
            if (!_disqual)
            {
                _disqual = true;
            }
        }
        public void Print()
        {
            if (!_disqual)
            {
                Console.WriteLine("{0,-7}| {1,-3} | {2,-3} | {3, -3}", _surname, _society, _res1, _res2);
            }

        }
        public static void Sort(Jumps[] contestants)
        {
            for (int i = 0; i < contestants.Length - 1; i++)
            {
                double amax = contestants[i]._totalres;
                int imax = i;
                for (int j = i + 1; j < contestants.Length; j++)
                {
                    if (contestants[j]._totalres > amax)
                    {
                        amax = contestants[j]._totalres;
                        imax = j;
                    }
                }
                Jumps temp;
                temp = contestants[imax];
                contestants[imax] = contestants[i];
                contestants[i] = temp;
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
            Jumps.Sort(contestants);
            contestants[0].Disqualification();
            contestants[3].Disqualification();
            Console.WriteLine("Сортированный список");
            for (int i = 0; i < contestants.Length; i++)
            {
                contestants[i].Print();
            }
        }
    }
}