namespace adventofcode2022;

public class DayTwelve
{
    public static async Task<int> Part1()
    {
        var lines = await File.ReadAllLinesAsync("testday12.txt");

        var columnCount = lines.Length;
        var rowCount = lines[0].Length;
        var moves = Directions();

        var (array, positionInitial, positionToFound) = GenerateCharArray(rowCount, columnCount, lines);

        return bfs(array, positionInitial, positionToFound, rowCount, columnCount, moves);
    }

    public static async Task<int> Part2()
    {
        var lines = await File.ReadAllLinesAsync("day12.txt");

        var columnCount = lines.Length;
        var rowCount = lines[0].Length;
        var moves = Directions();

        var (array, positionInitial, positionToFound) = GenerateCharArray(rowCount, columnCount, lines);

        var min = int.MaxValue;

        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                if (array[i, j] == 'a' && j == 0)
                    min = Math.Min(min, bfs(array, (i, j), positionToFound, rowCount, columnCount, moves));
            }
        }

        return min;
    }

    static (int, int)[] Directions()
        => new (int, int)[] { (0, -1), (0, 1), (1, 0), (-1, 0) };

    static (int, int) ApplyMove((int, int) move, (int, int) initial)
    {
        var (x, y) = initial;
        var (moveX, moveY) = move;
        return (x += moveX, y += moveY);
    }

    static int bfs(char[,] chars, (int, int) startPosition, (int, int) destPosition, int rowCount, int columnCount, (int, int)[] moves)
    {
        var visited = new bool[columnCount, rowCount];
        var steps = new int[columnCount, rowCount];
        var queue = new Queue<(int, int)>();
        queue.Enqueue(startPosition);
        visited[startPosition.Item1, startPosition.Item2] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var nextStep = steps[current.Item1, current.Item2] + 1;

            foreach (var direction in moves)
            {
                var (x, y) = ApplyMove(direction, current);

                if (x >= 0 && y >= 0 && x < columnCount && y < rowCount && !visited[x, y])
                {
                    var prev = chars[current.Item1, current.Item2];
                    var actual = chars[x, y];

                    if (actual - prev <= 1)
                    {
                        steps[x, y] = nextStep;
                        visited[x, y] = true;
                        queue.Enqueue((x, y));
                        if ((x, y) == destPosition)
                            return nextStep;
                    }
                }
            }
        }
        return 0;
    }

    static (char[,], (int, int), (int, int)) GenerateCharArray(int rowCount, int columnCount, string[] lines)
    {
        var array = new char[columnCount, rowCount];
        (int, int) positionToFound = (0, 0);
        (int, int) positionInitial = (0, 0);

        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                array[i, j] = lines[i][j];
                if (array[i, j] == 'S')
                {
                    positionInitial = (i, j);
                    array[i, j] = 'a';
                }
                if (array[i, j] == 'E')
                {
                    positionToFound = (i, j);
                    array[i, j] = 'z';
                }
            }
        }

        return (array, positionInitial, positionToFound);
    }
}
