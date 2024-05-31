using lab_9.Serializers;
using Microsoft.VisualBasic;
using System;
using ProtoBuf;
using System.Xml.Serialization;

[Serializable, ProtoContract]
[XmlInclude(typeof(JumpsHeight))]
[XmlInclude(typeof(JumpsLength))]
[ProtoInclude(10, typeof(JumpsHeight))]
[ProtoInclude(11, typeof(JumpsLength))]
public abstract class Discipline
{
    protected string _disname;
    protected string _surname;
    protected double _res1;
    protected double _res2;
    protected double _res3;
    protected double _bestres;
    [ProtoMember(1)]
    public string Disname { get { return _disname; } set { _disname = value; } }
    [ProtoMember(2)]
    public string Surname { get { return _surname; } set { _surname = value; } }
    [ProtoMember(3)]
    public double Res1 { get { return _res1;  } set { _res1 = value; } }
    [ProtoMember(4)]
    public double Res2 { get { return _res2; } set { _res2 = value; } }
    [ProtoMember(5)]
    public double Res3 { get { return _res3; } set { _res3 = value; } }
    [ProtoMember(6)]
    public double _best { get { return _bestres; } set { _bestres = value; } }
    public Discipline() { }
    public Discipline(string disname, string surname, double res1, double res2, double res3)
    {
        _disname = disname;
        _surname = surname;
        _res1 = res1;
        _res2 = res2;
        _res3 = res3;
        _bestres = 0;
    }
    static public void MergeSort(Discipline[] arr)
    {
        if (arr.Length <= 1)
            return;

        int mid = arr.Length / 2;

        Discipline[] left = new Discipline[mid];
        Discipline[] right = new Discipline[arr.Length - mid];

        for (int i = 0; i < mid; i++)
            left[i] = arr[i];

        for (int i = mid; i < arr.Length; i++)
            right[i - mid] = arr[i];

        MergeSort(left);
        MergeSort(right);

        Merge(arr, left, right);
    }
    public abstract void Print();
    static public void Merge(Discipline[] arr, Discipline[] left, Discipline[] right)
    {
        int i = 0, j = 0, k = 0;

        while (i < left.Length && j < right.Length)
        {
            if (left[i]._bestres <= right[j]._bestres)
                arr[k++] = left[i++];
            else
                arr[k++] = right[j++];
        }

        while (i < left.Length)
            arr[k++] = left[i++];

        while (j < right.Length)
            arr[k++] = right[j++];
    }
}
[ProtoContract]
public class JumpsHeight : Discipline
{
    public JumpsHeight() { }
    public JumpsHeight(string disname, string surname, double res1, double res2, double res3) : base(disname, surname, res1, res2, res3)
    {
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
[ProtoContract]
public class JumpsLength : Discipline
{
    public JumpsLength() { }
    public JumpsLength(string disname, string surname, double res1, double res2, double res3) : base(disname, surname, res1, res2, res3)
    {
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



public class Program
{
    public static void bestof3(Discipline[] contestants)
    {

        for (int i = 0; i < contestants.Length; i++)
        {
            if (contestants[i] is JumpsHeight)
            {
                JumpsHeight jumpsHeight = (JumpsHeight)contestants[i];
                double bestResult = Math.Max(jumpsHeight.res1, Math.Max(jumpsHeight.res2, jumpsHeight.res3));
                jumpsHeight._best = bestResult;
            }
            if (contestants[i] is JumpsLength)
            {
                JumpsLength jumpsLength = (JumpsLength)contestants[i];
                double bestResult = Math.Max(jumpsLength.res1, Math.Max(jumpsLength.res2, jumpsLength.res3));
                jumpsLength._best = bestResult;
            }

        }

    }

    public static void Main(string[] args)
    {

        JumpsHeight[] jh = new JumpsHeight[5]
        {
            new JumpsHeight("Прыжки в высоту", "Ivanov", 1, 4, 5),
            new JumpsHeight("Прыжки в высоту", "Petrov", 2, 2.5, 7),
            new JumpsHeight("Прыжки в высоту", "White", 3, 4, 6),
            new JumpsHeight("Прыжки в высоту", "Grey", 3, 2, 4),
            new JumpsHeight("Прыжки в высоту", "Black", 7, 9, 6),
        };
        JumpsLength[] jl = new JumpsLength[5]
        {
            new JumpsLength("Прыжки в длину", "Ivanov1", 1, 4, 5),
            new JumpsLength("Прыжки в длину", "Petrov1", 2, 2.5, 7),
            new JumpsLength("Прыжки в длину", "White1", 3, 4, 6),
            new JumpsLength("Прыжки в длину", "Grey1", 3, 2, 4),
            new JumpsLength("Прыжки в длину", "Black1", 7, 9, 6)
        };

        bestof3(jh);
        bestof3(jl);
        Discipline.MergeSort(jh);
        Discipline.MergeSort(jl);
        Console.WriteLine("Сортированный список прыжков в высоту");
        

        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string floderName = "lab92";
        path = Path.Combine(path, floderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        MySerializer[] serializers = { new MyJsonSerilazer(), new MyXmlSerilazer(), new MyBinSerilazer() };
        string[] files = { "jh.json", "jh.xml", "jh.bin",
        "jl.json", "jl.xml", "jl.bin"};
        for (int i = 0; i < serializers.Length; i++)
        {
            serializers[i].Write(jh, Path.Combine(path, files[i]));
            serializers[i].Write(jl, Path.Combine(path, files[i + 3]));
        }
        for (int i = 0; i < serializers.Length; i++)
        {
            var jH = serializers[i].Read<JumpsHeight[]>(Path.Combine(path, files[i]));
            foreach (var c in jH)
            {
                c.Print();
            }
            Console.WriteLine();
            var jL = serializers[i].Read<JumpsLength[]>(Path.Combine(path, files[i + 3]));
            foreach (var c in jL)
            {
                c.Print();
            }
            Console.WriteLine();
        }
    }
}


