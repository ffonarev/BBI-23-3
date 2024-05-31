using lab_9.Serializers;
using ProtoBuf;
using System;

[Serializable, ProtoContract]
public class Jumps
{
    private string _surname;
    private string _society;
    private double _res1;
    private double _res2;
    private double _totalres;
    private bool _disqual;
    [ProtoMember(1)]
    public string Surname { get { return _surname; } set { _surname = value; } }
    [ProtoMember(2)]
    public string Society { get { return _society; } set { _society = value; } }
    [ProtoMember(3)]
    public double Res1 { get { return _res1; } set { _res1 = value; } }
    [ProtoMember(4)]
    public double Res2 { get { return _res2;  } set { _res2 = value; } }
    [ProtoMember(5)]
    public double TotalRes { get { return _totalres; } set { _totalres = value; } }
    [ProtoMember(6)]
    public bool Disqual { get { return _disqual; } set { _disqual = value;   } }
    public Jumps() { }
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
public class Program
{
    public static void Main(string[] args)
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
           
        Jumps.Sort(contestants);
        contestants[0].Disqualification();
        contestants[3].Disqualification();
        Console.WriteLine("Сортированный список");
           
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string floderName = "lab91";
        path = Path.Combine(path, floderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        MySerializer[] serializers = {new MyJsonSerilazer(), new MyXmlSerilazer(), new MyBinSerilazer() };
        string[] files = { "contestants.json", "contestants.xml", "contestants.bin" };
        for (int i = 0; i < serializers.Length;i++) 
        {
            serializers[i].Write(contestants, Path.Combine(path, files[i]));
        }
        for (int i = 0; i<serializers.Length; i++)
        {
            var con = serializers[i].Read<Jumps[]>(Path.Combine(path, files[i]));
            foreach(var c in con)
            {
                c.Print();
            }
            Console.WriteLine();
        }
    }
}

