using System;

namespace lab3n3
{
    class Team
    {
        protected int[] _results;
        protected string _name;

        public Team(int[] results, string name)
        {
            _results = results;
            _name = name;
        }

        public int CalculatePoints()
        {
            int totalPoints = 0;
            for (int i = 0; i < _results.Length; i++)
            {
                if (_results[i] < 6)
                {
                    totalPoints += 5 + 1 - _results[i];
                }
            }
            return totalPoints;
        }

        public int FirstPlace()
        {
            int k = 0;
            for (int i = 0; i < _results.Length; i++)
            {
                if (_results[i] == 1)
                {
                    k = 1;
                }
            }
            return k;
        }
        public void DisplayResults()
        {
            Console.WriteLine("Результаты команды {0}:",_name);
            foreach (int result in _results)
            {
                Console.Write(result + " ");
            }
            Console.WriteLine();
        }
        public string GetName()
        {
            string k = "";
            k = _name;
            return k;
        }

        public void DisplayPoints()
        {
            Console.WriteLine("Итого баллов: {0}", CalculatePoints());
        }
        class MaleTeam : Team
        {
            public MaleTeam(int[] results, string name) : base(results, name) { }
        }
        class FemaleTeam : Team
        {
            public FemaleTeam(int[] results, string name) : base(results, name) { }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Мужские команды
            Team[] teamMale = new Team[3]
            {
                new Team(new int[] { 13, 8, 7, 2, 5, 6 }, "Мужская 1"),
                new Team(new int[] { 3, 4, 9, 10, 11, 12 }, "Мужская 2"),
                new Team(new int[] { 1, 14, 15, 16, 17, 18 }, "Мужская 3")
            };


            // Женские команды
            Team[] teamFemale = new Team[3]
            {
                new Team(new int[] { 9, 12, 3, 6, 2, 10 }, "Женская 1"),
                new Team(new int[] { 1, 4, 6, 10, 8, 7 }, "Женская 2"),
                new Team(new int[] { 8, 6, 7, 3, 2, 1 }, "Женская 3")
            };

            // Динамическая связка: Преобразование классов
            Team[] allTeams = new Team[6];
            allTeams[0] = teamFemale[0];
            allTeams[1] = teamFemale[1];
            allTeams[2] = teamFemale[2];
            allTeams[3] = teamMale[0];
            allTeams[4] = teamMale[1];
            allTeams[5] = teamMale[2];

            int[] tp = new int[6];
            for(int i = 0; i < 6; i++)
            {
                tp[i] = allTeams[i].CalculatePoints();
            }
            int max = -1;
            string k = "";
            for(int i = 0; i < 6; i++)
            {
                if(tp[i] > max) { max = tp[i]; k = allTeams[i].GetName(); }
            }

            Console.WriteLine("Результаты всех команд:");
            foreach (Team team in allTeams)
            {
                team.DisplayResults();
                team.DisplayPoints();
            }

            Console.WriteLine();
            Console.WriteLine("Winner: команда {0}", k);
            Console.WriteLine(max);

        }
    }
}