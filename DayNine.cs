namespace adventofcode2022;

public class DayNine
{
    public async static Task<int> Part1()
    {
        var result = 0;

        var lines = await ReadAndParseFile();

        var snake = new Snake((0, 0), (0, 0));

        foreach (var line in lines)
        {
            var instruction = line.Split(" ");
            var move = MapMove(instruction[0]);
            var amount = Convert.ToInt32(instruction[1]);

            while (amount > 0)
            {
                snake.MakeMove(move);
                amount--;
            }
        }

        result = snake.Visited.Count();

        return result;
    }

    public async static Task<int> Part2()
    {
        var DIR = new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

        var lines = await ReadAndParseFile(); ;

        var array = new (int, int)[10];
        var visited =new HashSet<(int, int)>();

        foreach (var line in lines)
        {
            var instruction = line.Split(" ");
            var move = MapMove(instruction[0]);
            var amount = Convert.ToInt32(instruction[1]);

            while (amount > 0)
            {
                var direction = DIR[(int)move];
                array[0].Item1 += direction.Item1;
                array[0].Item2 += direction.Item2;

                var (x1, y1) = array[0];

                for (int i = 1; i < array.Length; i++)
                {
                    var dx = x1 - array[i].Item1;
                    var dy = y1 - array[i].Item2;
                    var distance = Math.Max(Math.Abs(dx), Math.Abs(dy));

                    if (distance > 1)
                    {
                        array[i].Item1 += Math.Abs(dx) == 2 ? dx / 2 : dx;
                        array[i].Item2 += Math.Abs(dy) == 2 ? dy / 2 : dy;
                    }

                    x1 = array[i].Item1;
                    y1 = array[i].Item2;

                    if(i == array.Length -1) {
                        visited.Add((x1,y1));
                    }
                }

                amount--;
            }
        }

        return visited.Count;
    }

    static IEnumerable<IEnumerable<(int, int)>> ProduceItems((int, int)[] items, int windowSize)
    {
        for (int i = 0; i < items.Length; i += windowSize)
        {
            var chunkedItems = items.Skip(i * windowSize).Take(windowSize);

            yield return chunkedItems;
        }
    }

    static (int, int)[] ReturnChunkOfTwoItems((int, int)[] array, int index)
    {
        if (array.Length - 1 == index)
            return new (int, int)[] { };

        (int, int)[] tempArray = new (int, int)[2];

        for (int i = index; i <= index + 1; i++)
        {
            tempArray.Append(array[i]);
        }

        return tempArray;
    }

    async static Task<string[]> ReadAndParseFile()
        => await File.ReadAllLinesAsync("day9.txt");

    static Moves MapMove(string move)
         => move switch
         {
             "R" => Moves.Right,
             "L" => Moves.Left,
             "U" => Moves.Up,
             "D" => Moves.Down,
             _ => Moves.Right
         };
}

class Snake
{
    private (int, int)[] DIR = new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
    public (int, int) Head { get; set; }
    public (int, int) Tail { get; set; }
    public HashSet<(int, int)> Visited { get; set; } = new();

    public Snake((int, int) head, (int, int) tail)
    {
        Head = head;
        Tail = tail;
        Visited.Add(tail);
    }

    public void MakeMove(Moves move)
    {
        var direction = DIR[(int)move];
        var (x1, y1) = Head;
        x1 += direction.Item1;
        y1 += direction.Item2;
        Head = (x1, y1);

        var (x2, y2) = Tail;

        var dx = x1 - x2;
        var dy = y1 - y2;
        var distance = Math.Max(Math.Abs(dx), Math.Abs(dy));

        if (distance > 1)
        {
            (x2, y2) = Head;
            x2 -= direction.Item1;
            y2 -= direction.Item2;
        }

        Tail = (x2, y2);
        Visited.Add(Tail);
    }
}

enum Moves
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}