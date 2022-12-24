namespace adventofcode2022;

public class DayEight
{
    public static async Task<int> Part1()
    {
        var result = 0;

        var lines = await ReadAnParseFile();

        var totalLines = lines.Count();
        result += lines[0].Length;
        result += lines[totalLines - 1].Length;

        for (int j = 1; j < totalLines - 1; j++)
        {
            var line = lines[j];

            for (int i = 0; i < line.Length; i++)
            {
                if (i == 0 || i == line.Length - 1)
                {
                    result += 1;
                    continue;
                }

                if (isVisibleFromUpper(lines, j, i))
                {
                    result += 1;
                    continue;
                }

                if (isVisibleFromLeft(lines, j, i))
                {
                    result += 1;
                    continue;
                }

                if (isVisibleFromLower(lines, j, i))
                {
                    result += 1;
                    continue;
                }

                if (isVisibleFromRight(lines, j, i))
                {
                    result += 1;
                    continue;
                }
            }
        }

        return result;
    }

    public async static Task<int> Part2()
    {
        var result = 0;

        var lines = await ReadAnParseFile();

        var totalLines = lines.Count();

        for (int i = 1; i < totalLines - 1; i++)
        {
            var line = lines[i];

            for (int j = 1; j < line.Length - 1; j++)
            {

                var upper = totalTreesViewsFromUpper(lines, i, j);
                var lower = totalTreesViewsFromLower(lines, i, j);
                var left = totalTreesViewsFromLeft(lines, i, j);
                var right = totalTreesViewsFromRight(lines, i, j);

                var total = upper * lower * left * right;
                result = Math.Max(result, total);
            }
        }

        return result;
    }

    static int totalTreesViewsFromUpper(string[] lines, int i, int j)
    {
        var viewsCount = 0;
        var actualTree = Convert.ToInt32(lines[i][j].ToString());

        while (i > 0)
        {
            var upperTree = Convert.ToInt32(lines[i - 1][j].ToString());
            viewsCount++;
            if (upperTree >= actualTree)
                return viewsCount;
            i--;
        }

        return viewsCount;
    }

    static int totalTreesViewsFromLower(string[] lines, int i, int j)
    {
        var viewsCount = 0;
        var actualTree = Convert.ToInt32(lines[i][j].ToString());

        while (i < lines.Length - 1)
        {
            var lowerTree = Convert.ToInt32(lines[i + 1][j].ToString());
            viewsCount++;
            if (lowerTree >= actualTree)
                return viewsCount;
            i++;
        }

        return viewsCount;
    }

    static int totalTreesViewsFromLeft(string[] lines, int i, int j)
    {
        var viewsCount = 0;
        var actualTree = Convert.ToInt32(lines[i][j].ToString());

        while (j > 0)
        {
            var leftTree = Convert.ToInt32(lines[i][j - 1].ToString());
            viewsCount++;
            if (leftTree >= actualTree)
                return viewsCount;
            j--;
        }

        return viewsCount;
    }

    static int totalTreesViewsFromRight(string[] lines, int i, int j)
    {
        var viewsCount = 0;
        var actualTree = Convert.ToInt32(lines[i][j].ToString());

        while (j < lines[i].Length - 1)
        {
            var rightTree = Convert.ToInt32(lines[i][j + 1].ToString());
            viewsCount++;
            if (rightTree >= actualTree)
                return viewsCount;
            j++;
        }

        return viewsCount;
    }

    static bool isVisibleFromUpper(string[] lines, int j, int i)
    {
        var isVisible = false;
        var actualTree = Convert.ToInt32(lines[j][i].ToString());

        while (j > 0)
        {
            var upperTree = Convert.ToInt32(lines[j - 1][i].ToString());
            if (upperTree < actualTree)
                isVisible = true;
            else
            {
                isVisible = false;
                return isVisible;
            }
            j--;
        }

        return isVisible;
    }

    static bool isVisibleFromLower(string[] lines, int j, int i)
    {
        var isVisible = false;
        var actualTree = Convert.ToInt32(lines[j][i].ToString());

        while (j < lines.Count() - 1)
        {
            var lowerTree = Convert.ToInt32(lines[j + 1][i].ToString());
            if (lowerTree < actualTree)
                isVisible = true;
            else
            {
                isVisible = false;
                return isVisible;
            }
            j++;
        }

        return isVisible;
    }

    static bool isVisibleFromLeft(string[] lines, int j, int i)
    {
        var isVisible = false;
        var actualTree = Convert.ToInt32(lines[j][i].ToString());

        while (i > 0)
        {
            var leftTree = Convert.ToInt32(lines[j][i - 1].ToString());
            if (leftTree < actualTree)
                isVisible = true;
            else
            {
                isVisible = false;
                return isVisible;
            }
            i--;
        }

        return isVisible;
    }

    static bool isVisibleFromRight(string[] lines, int j, int i)
    {
        var isVisible = false;
        var actualTree = Convert.ToInt32(lines[j][i].ToString());

        while (i < lines[j].Length - 1)
        {
            var rightTree = Convert.ToInt32(lines[j][i + 1].ToString());
            if (rightTree < actualTree)
                isVisible = true;
            else
            {
                isVisible = false;
                return isVisible;
            }
            i++;
        }

        return isVisible;
    }

    static async Task<string[]> ReadAnParseFile()
        => await File.ReadAllLinesAsync("day8.txt");
}