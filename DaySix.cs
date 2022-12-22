namespace adventofcode2022;

public class DaySix
{
    public static async Task<int> Part1()
    {
        var result = 0;

        var lines = await ReadAndParseFile();
        var dict = new Dictionary<char, int>();
        var queue = new Queue<char>();

        for (int i = 0; i < lines[0].Length; i++)
        {
            var letter = lines[0][i];
            queue.Enqueue(letter);
            if (queue.Count > 4)
                _ = queue.Dequeue();

            if (queue.Count == 4 && isUnique(queue.ToArray()))
                return i + 1;
        }

        return result;
    }

    public static async Task<int> Part2()
    {
        var result = 0;

        var lines = await ReadAndParseFile();
        var dict = new Dictionary<char, int>();
        var queue = new Queue<char>();

        for (int i = 0; i < lines[0].Length; i++)
        {
            var letter = lines[0][i];
            queue.Enqueue(letter);
            if (queue.Count > 14)
                _ = queue.Dequeue();

            if (queue.Count == 14 && isUnique(queue.ToArray()))
                return i + 1;
        }

        return result;
    }

    static bool isUnique(char[] letters)
        => letters.Distinct().Count() == 14;

    static async Task<string[]> ReadAndParseFile()
        => await File.ReadAllLinesAsync("day6.txt");
}