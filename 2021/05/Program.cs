var input = File.ReadAllLines("input.txt");
var lines = input
    .Select(i => i.Split(" -> "))
    .Select(i => (Start: i[0].Split(","), End: i[1].Split(",")))
    .Select(i =>
        (
            X1: int.Parse(i.Start[0]),
            Y1: int.Parse(i.Start[1]),
            X2: int.Parse(i.End[0]),
            Y2: int.Parse(i.End[1])
        ));

var xMax = lines
    .Select(l => l.X1)
    .Concat(lines.Select(l => l.X2))
    .Max();
var yMax = lines
    .Select(l => l.Y1)
    .Concat(lines.Select(l => l.Y2))
    .Max();

var diagram = new int[xMax + 1, yMax + 1];
foreach (var (x1, y1, x2, y2) in lines)
{
    if (!(x1 == x2 || y1 == y2))
    {
        continue;
    }
    for (int x = 0; x < diagram.GetLength(0); x++)
    {
        for (int y = 0; y < diagram.GetLength(1); y++)
        {
            if (x >= Math.Min(x1, x2)
                && x <= Math.Max(x1, x2)
                && y >= Math.Min(y1, y2)
                && y <= Math.Max(y1, y2))
            {
                diagram[x, y]++;
            }
        }
    }
}
// PrintDiagram(diagram);
Console.WriteLine(TwoOrMoreOverlaps(diagram));

var diagram2 = new int[xMax + 1, yMax + 1];
foreach (var (x1, y1, x2, y2) in lines)
{
    var m = Slope(x1, y1, x2, y2);
    var b = YIntercept(m, x1, y1);
    for (int x = 0; x < diagram2.GetLength(0); x++)
    {
        for (int y = 0; y < diagram2.GetLength(1); y++)
        {
            if (x >= Math.Min(x1, x2)
                && x <= Math.Max(x1, x2)
                && y >= Math.Min(y1, y2)
                && y <= Math.Max(y1, y2))
            {
                if (x1 == x2 || y1 == y2)
                {
                    diagram2[x, y]++;
                }
                else if (!(x1 == x2 || y1 == y2) && y == m * x + b)
                {
                    diagram2[x, y]++;
                }
            }
        }
    }
}
// PrintDiagram(diagram2);
Console.WriteLine(TwoOrMoreOverlaps(diagram2));

static int Slope(int x1, int y1, int x2, int y2) => x2 - x1 == 0 ? 0 : (y2 - y1) / (x2 - x1);

static int YIntercept(int m, int x, int y) => y - m * x;

static int TwoOrMoreOverlaps(int[,] diagram)
{
    var overlaps = 0;
    for (int x = 0; x < diagram.GetLength(0); x++)
    {
        for (int y = 0; y < diagram.GetLength(1); y++)
        {
            if (diagram[x, y] >= 2)
            {
                overlaps++;
            }
        }
    }
    return overlaps;
}

// static void PrintDiagram(int[,] diagram)
// {
//     for (int y = 0; y < diagram.GetLength(0); y++)
//     {
//         for (int x = 0; x < diagram.GetLength(1); x++)
//         {
//             Console.Write(diagram[x, y] == 0 ? "." : diagram[x, y]);
//         }
//         Console.WriteLine();
//     }
// }