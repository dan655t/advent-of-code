var commands = File.ReadAllLines("input.txt");

var (horizontal1, depth1) = commands
    .Aggregate((Horizontal: 0, Depth: 0), (acc, cmd) =>
    {
        var result = ProcessCommand1(cmd);
        acc.Horizontal += result.Horizontal;
        acc.Depth += result.Depth;
        return acc;
    });
Console.WriteLine(horizontal1 * depth1);

var (horizontal2, depth2, aim2) = commands
    .Aggregate((Horizontal: 0, Depth: 0, Aim: 0), (acc, cmd) =>
    {
        var result = ProcessCommand2(cmd, acc.Aim);
        acc.Horizontal += result.Horizontal;
        acc.Depth += result.Depth;
        acc.Aim += result.Aim;
        return acc;
    });
Console.WriteLine(horizontal2 * depth2);

static (int Horizontal, int Depth) ProcessCommand1(string command)
{
    var commandParts = command.Split(" ");
    var direction = commandParts[0];
    var distance = int.Parse(commandParts[1]);
    return direction switch
    {
        "forward" => (Horizontal: distance, Depth: 0),
        "down" => (Horizontal: 0, Depth: distance),
        "up" => (Horizontal: 0, Depth: -distance),
        _ => (Horizontal: 0, Depth: 0),
    };
}

static (int Horizontal, int Depth, int Aim) ProcessCommand2(string command, int aim)
{
    var commandParts = command.Split(" ");
    var direction = commandParts[0];
    var distance = int.Parse(commandParts[1]);
    return direction switch
    {
        "forward" => (Horizontal: distance, Depth: aim * distance, Aim: 0),
        "down" => (Horizontal: 0, Depth: 0, Aim: distance),
        "up" => (Horizontal: 0, Depth: 0, Aim: -distance),
        _ => (Horizontal: 0, Depth: 0, Aim: 0),
    };
}