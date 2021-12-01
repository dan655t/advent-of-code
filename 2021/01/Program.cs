var lines = File.ReadAllLines("input.txt");

var (_, count1) = lines
    .Select(l => int.Parse(l))
    .Aggregate((Previous: int.MaxValue, Count: 0), (acc, value) => (value, value > acc.Previous ? ++acc.Count : acc.Count));
Console.WriteLine(count1);

var numbers = lines.Select(l => int.Parse(l));
var numbers2 = numbers.Skip(1);
var numbers3 = numbers.Skip(2);
var (_, count2) = numbers
    .Zip(numbers2)
    .Zip(numbers3)
    .Select(x => (n1: x.First.First, n2: x.First.Second, n3: x.Second))
    .Aggregate((Previous: int.MaxValue, Count: 0), (acc, windowParts) =>
    {
        var sum = windowParts.n1 + windowParts.n2 + windowParts.n3;
        return (sum, sum > acc.Previous ? ++acc.Count : acc.Count);
    });
Console.WriteLine(count2);