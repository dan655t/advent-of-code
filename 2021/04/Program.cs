var input = File.ReadAllLines("input.txt");
var numbers = input[0].Split(',').Select(int.Parse).ToArray();
var boards = LoadBoards(input.Skip(1).ToArray());
var numbersDrawn = new List<int>();
for (int i = 0; i < numbers.Length; i++)
{
    numbersDrawn.Add(numbers[i]);
    if (boards.Any(board => BoardIsWinner(board, numbersDrawn)))
    {
        var winningBoard = boards.First(board => BoardIsWinner(board, numbersDrawn));
        var winningBoardNumbers = winningBoard.Cast<int>().ToArray();
        var sumOfRemainingNumbers = winningBoardNumbers
            .Where(n => !numbersDrawn.Contains(n))
            .Aggregate(0, (acc, n) => acc += n);
        Console.WriteLine(sumOfRemainingNumbers * numbers[i]);
        break;
    }
}
numbersDrawn = new List<int>();
for (int i = 0; i < numbers.Length; i++)
{
    numbersDrawn.Add(numbers[i]);
    var winningBoardCount = boards.Count(board => BoardIsWinner(board, numbersDrawn));
    if (winningBoardCount == boards.Length) // the last board just won
    {
        var previousIndex = numbersDrawn.Count - 2;
        var lastBoardToWin = boards.First(board => !BoardIsWinner(board, numbersDrawn.ToArray()[..previousIndex]));
        var winningBoardNumbers = lastBoardToWin.Cast<int>().ToArray();
        var sumOfRemainingNumbers = winningBoardNumbers
            .Where(n => !numbersDrawn.Contains(n))
            .Aggregate(0, (acc, n) => acc += n);
        Console.WriteLine(sumOfRemainingNumbers * numbers[i]);
        break;
    }
}

static int[][,] LoadBoards(string[] boardData)
{
    var boards = new List<int[,]>();
    for (var i = 0; i < boardData.Length; i++)
    {
        if (string.IsNullOrEmpty(boardData[i]) || boardData[i] == "\r")
        {
            var start = i + 1;
            var end = i + 6;
            boards.Add(LoadBoard(boardData[start..end]));
        }
        continue;
    }
    return boards.ToArray();
}

static int[,] LoadBoard(string[] data)
{
    var board = new int[5, 5];
    for (var i = 0; i < 5; i++)
    {
        var row = data[i]
            .Split(' ')
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(int.Parse)
            .ToArray();
        for (var j = 0; j < 5; j++)
        {
            board[i, j] = row[j];
        }
    }
    return board;
}

static bool BoardIsWinner(int[,] board, IEnumerable<int> numbersDrawn)
{
    var lines = BoardRows(board).Concat(BoardColumns(board));
    return lines.Any(line => line.All(l => numbersDrawn.Contains(l)));
}

static IEnumerable<IEnumerable<int>> BoardColumns(int[,] board)
    => Enumerable.Range(0, board.GetLength(1))
        .Select(x => BoardColumn(board, x));

static IEnumerable<int> BoardColumn(int[,] board, int column)
    => Enumerable.Range(0, board.GetLength(0))
        .Select(x => board[x, column]);

static IEnumerable<IEnumerable<int>> BoardRows(int[,] board)
    => Enumerable.Range(0, board.GetLength(0))
        .Select(x => BoardRow(board, x));

static IEnumerable<int> BoardRow(int[,] board, int row)
    => Enumerable.Range(0, board.GetLength(1))
        .Select(x => board[row, x]);