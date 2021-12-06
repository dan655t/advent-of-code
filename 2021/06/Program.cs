var input = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse)
    .ToList();

var fishList = input.ToList();
for (var day = 0; day < 80; day++)
{
    var newFish = 0;
    for (var i = 0; i < fishList.Count; i++)
    {
        if (fishList[i] == 0)
        {
            fishList[i] = 6;
            newFish++;
        }
        else
        {
            fishList[i]--;
        }
    }
    fishList.AddRange(Enumerable.Range(0, newFish).Select(_ => 8));
}
Console.WriteLine(fishList.Count);

var fishByDay = Enumerable
    .Range(0, 9)
    .ToDictionary(i => i, i => input.LongCount(fishAge => fishAge == i));

for (var day = 0; day < 256; day++)
{
    fishByDay = new()
    {
        [0] = fishByDay[1],
        [1] = fishByDay[2],
        [2] = fishByDay[3],
        [3] = fishByDay[4],
        [4] = fishByDay[5],
        [5] = fishByDay[6],
        [6] = fishByDay[7] + fishByDay[0],
        [7] = fishByDay[8],
        [8] = fishByDay[0],
    };
}
Console.WriteLine(fishByDay.Values.Sum());
