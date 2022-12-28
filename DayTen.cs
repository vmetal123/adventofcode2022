namespace adventofcode2022;

public class DayTen
{
    public async static Task<int> Part1()
    {
        var result = 1;

        var lines = await File.ReadAllLinesAsync("day10.txt");
        var cycles = 0;
        var cycleVerification = 20;
        var cycleVerificationIncrement = 40;
        var signalStrength = 0;

        foreach (var line in lines)
        {
            var instruction = line.Split(" ");

            if (instruction[0] == "addx")
            {
                var cycleCount = 0;
                while (cycleCount < 2)
                {
                    cycles++;
                    if (cycles == cycleVerification)
                    {
                        signalStrength += (cycles * result);
                        cycleVerification += cycleVerificationIncrement;
                    }
                    cycleCount++;
                }
                result += Convert.ToInt32(instruction[1]);
            }

            if (instruction[0] == "noop")
            {
                cycles++;
                if (cycles == cycleVerification)
                {
                    signalStrength += (cycles * result);
                    cycleVerification += cycleVerificationIncrement;
                }
            }
        }

        return signalStrength;
    }

    public async static Task<int> Part2()
    {
        var lines = await File.ReadAllLinesAsync("day10.txt");

        char[] start = $"###{new string('.', 37)}".ToArray();
        var CRT = new List<char[]>();
        var cycles = 0;
        var cycleVerification = 40;
        var registerX = 1;
        var crtLine = new char[start.Length];

        foreach (var line in lines)
        {
            var instruction = line.Split(" ");

            if (instruction[0] == "addx")
            {
                var cycleCount = 2;
                while (cycleCount > 0)
                {
                    cycleCount--;
                    cycles++;
                    crtLine[cycles - 1] = start[cycles - 1];
                    if (cycles == cycleVerification)
                    {
                        cycles = 0;
                        CRT.Add(crtLine);
                        PrintCRTLine(crtLine);
                        crtLine = new char[start.Length];
                    }
                }
                registerX += Convert.ToInt32(instruction[1]);
                start = GenerateNextCRT(registerX);
            }

            if (instruction[0] == "noop")
            {
                cycles++;
                crtLine[cycles - 1] = start[cycles - 1];
                if (cycles == cycleVerification)
                {
                    cycles = 0;
                    CRT.Add(crtLine);
                    PrintCRTLine(crtLine);
                    crtLine = new char[start.Length];
                }
            }


        }

        return 0;
    }

    static char[] GenerateNextCRT(int index)
    {
        var startNumberOfDots = index - 1;
        var numberOfSharps = 3;

        if (index < 0)
        {
            startNumberOfDots = 0;
            numberOfSharps += index;
        }

        if (startNumberOfDots < 0)
            startNumberOfDots = 0;

        var endNumberOfDots = 40 - (startNumberOfDots + numberOfSharps);

        if (endNumberOfDots < 0)
            endNumberOfDots = 0;

        return $"{new String('.', startNumberOfDots)}{new String('#', numberOfSharps)}{new string('.', endNumberOfDots)}".ToArray();
    }

    static void PrintCRTLine(char[] crtLine)
    {
        Console.WriteLine(string.Join("", crtLine));
    }
}