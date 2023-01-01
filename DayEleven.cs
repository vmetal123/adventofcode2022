namespace adventofcode2022;

public class DayEleven
{
    public async static Task<long> Part1()
    {
        var lines = await File.ReadAllLinesAsync("testday11.txt");
        var monkeys = new List<Monkey>();

        var monkey = new Monkey();

        foreach (var line in lines)
        {
            if (line.Contains("Monkey"))
            {
                var monkeyInfo = line.Split(" ");
                var numberMonkey = monkeyInfo[1].Substring(0, 1);
                monkey.Number = Convert.ToInt32(numberMonkey);
                continue;
            }

            if (line.Trim().StartsWith("Starting items:"))
            {
                var itemsInfo = line.Trim().Substring(15);
                monkey.Items = itemsInfo.Trim().Split(",").Select(x => Convert.ToInt64(x)).ToList();
                continue;
            }

            if (line.Trim().StartsWith("Operation: new = old"))
            {
                var operationInfo = line.Trim().Substring(20);
                var operation = operationInfo.Trim().Split(" ");
                if (Int32.TryParse(operation[1], out var value))
                {
                    monkey.OperationValue = value;
                    monkey.Operation = operation[0] switch
                    {
                        "+" => Operation.Sum,
                        "*" => Operation.Multiply,
                        _ => throw new InvalidOperationException()
                    };
                }
                else
                    monkey.Operation = Operation.Square;

                continue;
            }

            if (line.Trim().StartsWith("Test: divisible by"))
            {
                var divisibleInfo = line.Trim().Substring(18);
                monkey.Divisible = Convert.ToInt32(divisibleInfo.Trim());
                continue;
            }

            if (line.Trim().StartsWith("If true: throw to monkey"))
            {
                var passInfo = line.Trim().Substring(24);
                monkey.Pass = Convert.ToInt32(passInfo.Trim());
                continue;
            }

            if (line.Trim().StartsWith("If false: throw to monkey"))
            {
                var failInfo = line.Trim().Substring(25);
                monkey.Fail = Convert.ToInt32(failInfo.Trim());
                monkeys.Add(monkey);
                monkey = new Monkey();
                continue;
            }

            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }
        }

        for (int i = 0; i < 20; i++)
        {
            foreach (var obj in monkeys)
            {
                foreach (var item in obj.Items)
                {
                    var result = obj.Operation switch
                    {
                        Operation.Sum => item + obj.OperationValue,
                        Operation.Multiply => item * obj.OperationValue,
                        Operation.Square => item * item,
                        _ => throw new InvalidOperationException()
                    };

                    long worryLevel = result / 3;
                    var rest = worryLevel % obj.Divisible;

                    if (rest == 0)
                    {
                        monkeys.Where(x => x.Number == obj.Pass).First().Items.Add(worryLevel);
                    }
                    else
                    {
                        monkeys.Where(x => x.Number == obj.Fail).First().Items.Add(worryLevel);
                    }
                    obj.Inspections++;
                }

                obj.Items = new List<long>();
            }
        }

        var toptwo = monkeys
            .OrderByDescending(x => x.Inspections)
            .Take(2).Select(x => x.Inspections)
            .ToArray();

        return toptwo[0] * toptwo[1];
    }

    public async static Task<long> Part2()
    {
        var lines = await File.ReadAllLinesAsync("day11.txt");
        var monkeys = new List<Monkey>();

        var monkey = new Monkey();

        foreach (var line in lines)
        {
            if (line.Contains("Monkey"))
            {
                var monkeyInfo = line.Split(" ");
                var numberMonkey = monkeyInfo[1].Substring(0, 1);
                monkey.Number = Convert.ToInt32(numberMonkey);
                continue;
            }

            if (line.Trim().StartsWith("Starting items:"))
            {
                var itemsInfo = line.Trim().Substring(15);
                monkey.Items = itemsInfo.Trim().Split(",").Select(x => Convert.ToInt64(x)).ToList();
                continue;
            }

            if (line.Trim().StartsWith("Operation: new = old"))
            {
                var operationInfo = line.Trim().Substring(20);
                var operation = operationInfo.Trim().Split(" ");
                if (Int32.TryParse(operation[1], out var value))
                {
                    monkey.OperationValue = value;
                    monkey.Operation = operation[0] switch
                    {
                        "+" => Operation.Sum,
                        "*" => Operation.Multiply,
                        _ => throw new InvalidOperationException()
                    };
                }
                else
                    monkey.Operation = Operation.Square;

                continue;
            }

            if (line.Trim().StartsWith("Test: divisible by"))
            {
                var divisibleInfo = line.Trim().Substring(18);
                monkey.Divisible = Convert.ToInt32(divisibleInfo.Trim());
                continue;
            }

            if (line.Trim().StartsWith("If true: throw to monkey"))
            {
                var passInfo = line.Trim().Substring(24);
                monkey.Pass = Convert.ToInt32(passInfo.Trim());
                continue;
            }

            if (line.Trim().StartsWith("If false: throw to monkey"))
            {
                var failInfo = line.Trim().Substring(25);
                monkey.Fail = Convert.ToInt32(failInfo.Trim());
                monkeys.Add(monkey);
                monkey = new Monkey();
                continue;
            }

            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }
        }

        long commonMultiple = 1;
        foreach (var item in monkeys)
        {
            commonMultiple *= item.Divisible;
        }

        for (int i = 0; i < 10000; i++)
        {
            foreach (var obj in monkeys)
            {
                foreach (var item in obj.Items)
                {
                    obj.Inspections++;
                    var result = obj.Operation switch
                    {
                        Operation.Sum => item + obj.OperationValue,
                        Operation.Multiply => item * obj.OperationValue,
                        Operation.Square => item * item,
                        _ => throw new InvalidOperationException()
                    };

                    long worryLevel = result % commonMultiple;
                    var rest = worryLevel % obj.Divisible;

                    if (rest == 0)
                    {
                        monkeys.Where(x => x.Number == obj.Pass).First().Items.Add(worryLevel);
                    }
                    else
                    {
                        monkeys.Where(x => x.Number == obj.Fail).First().Items.Add(worryLevel);
                    }
                }

                obj.Items = new List<long>();
            }
        }

        var toptwo = monkeys
            .OrderByDescending(x => x.Inspections)
            .Select(x => x.Inspections)
            .ToArray();

        return toptwo[0] * toptwo[1];
    }
}

class Monkey
{
    public int Number { get; set; }
    public List<long> Items { get; set; } = new();
    public Operation Operation { get; set; }
    public long OperationValue { get; set; }
    public long Divisible { get; set; }
    public int Pass { get; set; }
    public int Fail { get; set; }
    public long Inspections { get; set; }
}

enum Operation
{
    Sum,
    Multiply,
    Square
}