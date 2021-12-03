var lines = File.ReadAllLines("input.txt");
var wordLength = lines[0].Length;

var mostCommons = lines
    .Aggregate(new int[wordLength], (acc, line) =>
        acc
            .Zip(line)
            .Select(l => l.First + int.Parse($"{l.Second}"))
            .ToArray()
        )
    .Select(i => i > lines.Length / 2);

var gamma = new string(mostCommons.SelectMany(x => x ? "1" : "0").ToArray());
var epsilon = new string(mostCommons.SelectMany(x => x ? "0" : "1").ToArray());
Console.WriteLine(Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));

var oxygenRating = GetRating(lines, (zeroes, ones) => zeroes > ones ? '0' : '1');
var scrubberRating = GetRating(lines, (zeroes, ones) => zeroes > ones ? '1' : '0');
Console.WriteLine(oxygenRating * scrubberRating);

static int GetRating(string[] lines, Func<int, int, char> keepCharFunc)
{
    var wordLength = lines[0].Length;
    var remainingLines = lines.ToList();
    for (var i = 0; i < wordLength; i++)
    {
        var zeroes = remainingLines.Count(l => l[i] == '0');
        var ones = remainingLines.Count(l => l[i] == '1');
        var keepChar = keepCharFunc(zeroes, ones);
        remainingLines = remainingLines
            .Where(l => l[i] == keepChar)
            .ToList();
        if (remainingLines.Count == 1)
        {
            break;
        }
    }
    return Convert.ToInt32(remainingLines[0], 2);
}