namespace adventofcode2022;

public class DayFour
{
    public async Task<int> Part1()
    {
        var sum = 0;

        var list = await ReadAndParseFile();
        var assignments = GetPairsAssignments(list);

        foreach (var pair in assignments)
        {
            sum += IsAssignmentPairContain(pair) ? 1 : 0;
        }

        return sum;
    }

    public async Task<int> Part2()
    {
        var sum = 0;

        var list = await ReadAndParseFile();
        var assignments = GetPairsAssignments(list);

        foreach (var pair in assignments)
        {
            sum += IsAssignmentPairOverlapped(pair) ? 1 : 0;
        }

        return sum;
    }
    static async Task<List<string>> ReadAndParseFile()
    {
        var text = await File.ReadAllTextAsync("day4.txt");

        var list = text.Split("\r\n").ToList();

        return list;
    }

    static List<(string, string)> GetPairsAssignments(List<string> list)
        => list.Select(x =>
        {
            var assignments = x.Split(",");

            return (assignments[0], assignments[1]);
        }).ToList();

    static bool IsAssignmentPairContain((string, string) pair)
    {
        var (first, second) = pair;

        var firstList = ProcessAssignment(first);
        var secondList = ProcessAssignment(second);

        if (firstList.All(x => secondList.Contains(x)) || secondList.All(x => firstList.Contains(x)))
            return true;

        return false;
    }

    static bool IsAssignmentPairOverlapped((string, string) pair)
    {
        var (first, second) = pair;

        var firstList = ProcessAssignment(first);
        var secondList = ProcessAssignment(second);

        if (firstList.Any(x => secondList.Contains(x)) || secondList.Any(x => firstList.Contains(x)))
            return true;

        return false;
    }

    static List<int> ProcessAssignment(string assignment)
    {
        var startEnd = assignment.Split("-");

        var start = Convert.ToInt32(startEnd[0]);
        var end = Convert.ToInt32(startEnd[1]);
        var count = (end - start) + 1;

        return Enumerable.Range(start, count).ToList();
    }
}