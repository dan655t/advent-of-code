var positions = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse);

var fuelCost1 = LowestFuelCost(positions, distance => distance);
Console.WriteLine(fuelCost1);
var fuelCost2 = LowestFuelCost(positions, distance => distance * (distance + 1) / 2);
Console.WriteLine(fuelCost2);

static int LowestFuelCost(IEnumerable<int> positions, Func<int, int> fuelCost)
    => positions
        .Select(pos => positions.Select(dest => fuelCost(Math.Abs(pos - dest))).Sum())
        .OrderBy(x => x)
        .First();
