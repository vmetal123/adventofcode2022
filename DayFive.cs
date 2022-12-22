namespace adventofcode2022;

public class DayFive
{
    public static async Task<string> Part1()
    {
        var lines = await ReadFile();

        var separatorIndex = Array.IndexOf(lines, string.Empty);

        var numberOfStacks = getNumberOfStacks(lines, separatorIndex);

        var stacks = new Stack<char>[numberOfStacks];

        for (int i = separatorIndex - 2; i >= 0; i--)
        {
            var line = lines[i];

            for (int stackId = 0; stackId < numberOfStacks; stackId++)
            {
                var column = line[stackId * 4 + 1];
                if (char.IsLetter(column))
                {
                    stacks[stackId] ??= new Stack<char>();
                    stacks[stackId].Push(column);
                }
            }
        }

        for (int i = separatorIndex + 1; i < lines.Length; i++)
        {
            var instruction = lines[i].Split(' ');
            var toMove = Convert.ToInt32(instruction[1]);
            var from = Convert.ToInt32(instruction[3]);
            var to = Convert.ToInt32(instruction[5]);

            for (int moveCount = 0; moveCount < toMove; moveCount++)
            {
                var box = stacks[from - 1].Pop();
                stacks[to - 1].Push(box);
            }
        }

        var result = string.Empty;
        for (int i = 0; i < stacks.Length; i++)
        {
            var letter = stacks[i].Pop();
            result += letter.ToString();
        }

        return result;
    }

    public static async Task<string> Part2()
    {
        var result = string.Empty;

        var lines = await ReadFile();

        var separatorIndex = Array.IndexOf(lines, string.Empty);

        var numberOfStacks = getNumberOfStacks(lines, separatorIndex);

        var stacks = new Stack<char>[numberOfStacks];

        for (int i = separatorIndex - 2; i >= 0; i--)
        {
            var line = lines[i];

            for (int stackId = 0; stackId < numberOfStacks; stackId++)
            {
                var column = line[stackId * 4 + 1];
                if (char.IsLetter(column))
                {
                    stacks[stackId] ??= new Stack<char>();
                    stacks[stackId].Push(column);
                }
            }
        }

        for (int i = separatorIndex + 1; i < lines.Length; i++)
        {
            var instruction = lines[i].Split(' ');
            var toMove = Convert.ToInt32(instruction[1]);
            var from = Convert.ToInt32(instruction[3]);
            var to = Convert.ToInt32(instruction[5]);

            if (toMove == 1)
            {
                var box = stacks[from - 1].Pop();
                stacks[to - 1].Push(box);
            }
            else
            {
                var auxStack = new Stack<char>();
                for (int moveCount = 0; moveCount < toMove; moveCount++)
                {
                    var box = stacks[from - 1].Pop();
                    auxStack.Push(box);
                }

                for (int moveCount = 0; moveCount < toMove; moveCount++)
                {
                    stacks[to - 1].Push(auxStack.Pop());
                }
            }
        }

        for (int i = 0; i < stacks.Length; i++)
        {
            var letter = stacks[i].Pop();
            result += letter.ToString();
        }

        return result;
    }

    static async Task<string[]> ReadFile()
        => await File.ReadAllLinesAsync("day5.txt");

    static int getNumberOfStacks(string[] lines, int separatorIndex)
        => lines[separatorIndex - 1]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => Convert.ToInt32(x))
            .Last();
}