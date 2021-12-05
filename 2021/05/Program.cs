var input = File.ReadAllLines("input.txt");
var lines = input
    .Select(i => i.Split(" -> "))
    .Select(i => (Start: i[0].Split(","), End: i[1].Split(",")))
    .Select(i => new Line
        (
            Start: (X: int.Parse(i.Start[0]), Y: int.Parse(i.Start[1])),
            End: (X: int.Parse(i.End[0]), Y: int.Parse(i.End[1]))
        ));

var xMax = lines
    .Select(l => l.Start.X)
    .Concat(lines.Select(l => l.End.X))
    .Max();
var yMax = lines
    .Select(l => l.Start.Y)
    .Concat(lines.Select(l => l.End.Y))
    .Max();

var diagram = new int[xMax + 1, yMax + 1];

foreach (var line in lines)
{
    if (!(line.Start.X == line.End.X || line.Start.Y == line.End.Y))
    {
        Console.WriteLine(line);
        continue;
    }
    for (int x = 0; x < diagram.GetLength(1); x++)
    {
        for (int y = 0; y < diagram.GetLength(0); y++)
        {
            if (line.Start.X <= x
                && line.End.X >= x
                && line.Start.Y <= y
                && line.End.Y >= y)
            {
                diagram[x, y]++;
            }
        }
    }
}

PrintDiagram(diagram);

static void PrintDiagram(int[,] diagram)
{
    for (int x = 0; x < diagram.GetLength(1); x++)
    {
        for (int y = 0; y < diagram.GetLength(0); y++)
        {
            Console.Write(diagram[x, y]);
        }
        Console.WriteLine();
    }
}

record Line((int X, int Y) Start, (int X, int Y) End);