using lab_9.Serializers;
using ProtoBuf;
using System;
using System.Xml.Serialization;

[Serializable, ProtoContract]
[ProtoInclude(104,typeof(MaleTeam))]
[ProtoInclude(105,typeof(FemaleTeam))]
[XmlInclude(typeof(MaleTeam))]
[XmlInclude(typeof(FemaleTeam))]

public class Team
{
    protected int[] _results;
    protected string _name;
    [ProtoMember(1)]
    public int[] Result { get { return _results; } set { _results = value; } }
    [ProtoMember(2)]
    public string Name { get { return _name; } set { _name = value; } }
    public Team() { }

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
        Console.WriteLine("Результаты команды {0}:", _name);
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
   

}
[ProtoContract]
public class MaleTeam : Team
{
    public MaleTeam() { }
    public MaleTeam(int[] results, string name) : base(results, name) { }
}
[ProtoContract]
public class FemaleTeam : Team
{
    public FemaleTeam() { }
    public FemaleTeam(int[] results, string name) : base(results, name) { }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Мужские команды
        MaleTeam[] teamMale = new MaleTeam[3]
        {
            new MaleTeam(new int[] { 13, 8, 7, 2, 5, 6 }, "Мужская 1"),
            new MaleTeam(new int[] { 3, 4, 9, 10, 11, 12 }, "Мужская 2"),
            new MaleTeam(new int[] { 1, 14, 15, 16, 17, 18 }, "Мужская 3")
        };


        // Женские команды
        FemaleTeam[] teamFemale = new FemaleTeam[3]
        {
            new FemaleTeam(new int[] { 9, 12, 3, 6, 2, 10 }, "Женская 1"),
            new FemaleTeam(new int[] { 1, 4, 6, 10, 8, 7 }, "Женская 2"),
            new FemaleTeam(new int[] { 8, 6, 7, 3, 2, 1 }, "Женская 3")
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
        for (int i = 0; i < 6; i++)
        {
            tp[i] = allTeams[i].CalculatePoints();
        }
        int max = -1;
        string k = "";
        for (int i = 0; i < 6; i++)
        {
            if (tp[i] > max) { max = tp[i]; k = allTeams[i].GetName(); }
        }

        Console.WriteLine("Результаты всех команд:");
        foreach (Team team in allTeams)
        {
            team.DisplayResults();
            team.DisplayPoints();
        }



        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string floderName = "lab93";
        path = Path.Combine(path, floderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        MySerializer[] serializers = { new MyJsonSerilazer(), new MyXmlSerilazer(), new MyBinSerilazer() };
        string[] files = { "teamMale.json", "teamMale.xml", "teamMale.bin",
        "teamFemale.json", "teamFemale.xml", "teamFemale.bin",
        "allTeams.json", "allTeams.xml", "allTeams.bin"};
        for (int i = 0; i < serializers.Length; i++)
        {
            serializers[i].Write(teamMale, Path.Combine(path, files[i]));
            serializers[i].Write(teamFemale, Path.Combine(path, files[i + 3]));
            serializers[i].Write(allTeams, Path.Combine(path, files[i + 6]));
        }
        for (int i = 0; i < serializers.Length; i++)
        {
            var Tm = serializers[i].Read<MaleTeam[]>(Path.Combine(path, files[i]));
            foreach (var c in Tm)
            {
                c.DisplayResults();
                c.DisplayPoints();
            }
            Console.WriteLine();
            var Tf = serializers[i].Read<FemaleTeam[]>(Path.Combine(path, files[i + 3]));
            foreach (var c in Tf)
            {
                c.DisplayResults();
                c.DisplayPoints();
            }
            Console.WriteLine();
            var aT = serializers[i].Read<Team[]>(Path.Combine(path, files[i + 6]));
            foreach (var c in aT)
            {
                c.DisplayResults();
                c.DisplayPoints();
            }
            Console.WriteLine();
        }

        
        Console.WriteLine("Winner: команда {0}", k);
        Console.WriteLine(max);

    }
}

