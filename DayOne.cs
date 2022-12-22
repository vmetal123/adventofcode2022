namespace adventofcode2022;

public class DayOne
{
    public async Task<int> GetMaxCaloriesFromElves()
    {
        var list = await GetLinesFromFile();

        return list.Max();
    }

    public async Task<int> GetTopThreeMaxCaloriesFromElves()
    {
        var list = await GetLinesFromFile();

        list = list.OrderByDescending(x => x).ToList();

        var sum = 0;
        for (int i = 0; i < 3; i++)
        {
            sum += list[i];
        }

        return sum;
    }

    private async Task<List<int>> GetLinesFromFile()
    {
        var file = await File.ReadAllTextAsync("day1.txt");

        var lines = file.Replace("\r", "").Split("\n").ToList();

        var list = new List<int>();
        var sum = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                list.Add(sum);
                sum = 0;
            }
            else
            {
                sum += Convert.ToInt32(line);
            }
        }

        return list;
    }
}