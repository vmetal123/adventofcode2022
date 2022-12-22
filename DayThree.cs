namespace adventofcode2022;
public class DayThree
{

    public async ValueTask<int> Part1()
    {
        var list = await ReadAndParseFile();
        var letters = getCharsWithVal();
        var sum = 0;

        foreach (var item in list)
        {
            var middle = item.Length / 2;

            var first = item.Substring(0, middle).ToCharArray().Distinct();
            var second = item.Substring(middle).ToCharArray().Distinct();

            var commonChar = first.Where(x => second.Any(y => y == x)).Select(x => x).First();

            sum += letters.Where(x => x.Item1 == commonChar).Select(x => x.Item2).First();
        }

        return sum;
    }

    public async Task<int> Part2()
    {
        var list = await ReadAndParseFile();
        var letters = getCharsWithVal();
        var newList = list.Chunk(3).ToList();
        var sum = 0;

        foreach (var item in newList)
        {
            var groups = item.Select(x => x.Distinct()).ToList();
            var commonChars = groups[0];

            for (var i = 1; i < groups.Count; i++)
                commonChars = commonChars.Where(x => groups[i].Any(y => y == x)).ToList();

            sum += letters.Where(x => x.Item1 == commonChars.First()).Select(x => x.Item2).First();
        }

        return sum;
    }

    async ValueTask<List<string>> ReadAndParseFile()
    {
        var text = await File.ReadAllTextAsync("day3.txt");

        var list = text.Split("\r\n").ToList();

        return list;
    }
    List<(char, int)> getCharsWithVal()
    {
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        var lowerLetters = letters.ToLower().ToCharArray().ToList();
        var upperLetters = letters.ToCharArray().ToList();

        var list = new List<(char, int)>();
        var i = 0;

        lowerLetters.ForEach(x => list.Add((x, ++i)));
        upperLetters.ForEach(x => list.Add((x, ++i)));

        return list;
    }
}
