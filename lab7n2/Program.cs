using System;

namespace lab7n2
{
    abstract class Discipline
    {
        protected string _disname;
        protected string _surname;
        protected double _res1;
        protected double _res2;
        protected double _res3;
        public double _bestres;
        public Discipline(string disname, string surname, double res1, double res2, double res3)
        {
            _disname = disname;
            _surname = surname;
            _res1 = res1;
            _res2 = res2;
            _res3 = res3;
            _bestres = 0;
        }
        public abstract void Print();
    }
    class JumpsHeight : Discipline
    {
        public JumpsHeight(string disname, string surname, double res1, double res2, double res3) : base(disname, surname, res1, res2, res3)
        {
            _surname = surname;
            _res1 = res1;
            _res2 = res2;
            _res3 = res3;
            _bestres = 0;
        }
        public double res1 => _res1;
        public double res2 => _res2;
        public double res3 => _res3;
        public override void Print()
        {
            Console.WriteLine("Дисциплина: {0}", _disname);
            Console.WriteLine("{0,-7} | {1,-3}", _surname, _bestres);
        }
    }
    class JumpsLength : Discipline
    {
        public JumpsLength(string disname, string surname, double res1, double res2, double res3) : base(disname, surname, res1, res2, res3)
        {
            _surname = surname;
            _res1 = res1;
            _res2 = res2;
            _res3 = res3;
            _bestres = 0;
        }
        public double res1 => _res1;
        public double res2 => _res2;
        public double res3 => _res3;
        public override void Print()
        {
            Console.WriteLine("Дисциплина: {0}", _disname);
            Console.WriteLine("{0,-7} | {1,-3}", _surname, _bestres);
        }
    }
    class Program
    {
        static void bestof3(Discipline[] contestants)
        {

            for (int i = 0; i < contestants.Length; i++)
            {
                if (contestants[i] is JumpsHeight)
                {
                    JumpsHeight jumpsHeight = (JumpsHeight)contestants[i];
                    double bestResult = Math.Max(jumpsHeight.res1, Math.Max(jumpsHeight.res2, jumpsHeight.res3));
                    jumpsHeight._bestres = bestResult;
                }
                if (contestants[i] is JumpsLength)
                {
                    JumpsLength jumpsLength = (JumpsLength)contestants[i];
                    double bestResult = Math.Max(jumpsLength.res1, Math.Max(jumpsLength.res2, jumpsLength.res3));
                    jumpsLength._bestres = bestResult;
                }

            }

        }
        static void Main(string[] args)
        {
            Discipline[] contestants = new Discipline[]
            {
                new JumpsHeight("Прыжки в высоту", "Ivanov", 1, 4, 5),
                new JumpsHeight("Прыжки в высоту", "Petrov", 2, 2.5, 7),
                new JumpsHeight("Прыжки в высоту", "White", 3, 4, 6),
                new JumpsHeight("Прыжки в высоту", "Grey", 3, 2, 4),
                new JumpsHeight("Прыжки в высоту", "Black", 7, 9, 6),
                new JumpsLength("Прыжки в длину", "Ivanov1", 1, 4, 5),
                new JumpsLength("Прыжки в длину", "Petrov1", 2, 2.5, 7),
                new JumpsLength("Прыжки в длину", "White1", 3, 4, 6),
                new JumpsLength("Прыжки в длину", "Grey1", 3, 2, 4),
                new JumpsLength("Прыжки в длину", "Black1", 7, 9, 6)
            };

            bestof3(contestants);
            Console.WriteLine("Сортированный список");
            for (int i = 0; i < contestants.Length; i++)
            {
                contestants[i].Print();
            }
        }
    }
}
