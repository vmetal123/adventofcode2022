namespace adventofcode2022;

public class DayTwo
{
    public async Task<int> GetTotalScore()
    {
        var sum = 0;

        try
        {
            var rounds = await ReadAndParseFile();

            var opp = GenerateOpponent();
            var you = GenerateYou();

            foreach (var item in rounds)
            {
                var Opp = opp[item.Item1];
                var You = you[item.Item2];

                sum += MapPlayToVal(item.Item2, you);

                sum += returnPlayResult(Opp, You);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return sum;
    }

    public async Task<int> Part2()
    {
        var sum = 0;

        var rounds = await ReadAndParseFile();
        //var rounds = new List<(string, string)>
        //{
        //    ("A","Y"),
        //    ("B","X"),
        //    ("C","Z")
        //};
        var oppPlay = GenerateOpponent();
        var mePlay = GenerateYou();

        foreach (var round in rounds)
        {
            var (opp, me) = round;
            var oppTurn = oppPlay[opp];

            var meTurn = returnPlay(oppTurn, me)!;
            var meVal = mePlay.Where(x => x.Value == meTurn).Select(x => x.Key).First();
            var result = getResultByValue(me);
            var playResult = MapPlayToVal(meVal, mePlay);
            sum += result + playResult;
        }

        return sum;
    }

    private async Task<List<(string, string)>> ReadAndParseFile()
    {
        var result = new List<(string, string)>();

        try
        {
            var text = await File.ReadAllTextAsync("day2.txt");

            var list = text
                .Replace("\r", "")
                .Split("\n");

            foreach (var item in list)
            {
                var plays = item.Split(" ");
                result.Add((plays[0].ToString(), plays[1].ToString()));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return result;
    }

    Dictionary<string, Plays> GenerateOpponent() => new Dictionary<string, Plays> {
        { "A", Plays.Rock },
        { "B", Plays.Paper },
        { "C", Plays.Scissors }
    };

    Dictionary<string, Plays> GenerateYou() => new Dictionary<string, Plays> {
        { "X", Plays.Rock },
        { "Y", Plays.Paper },
        { "Z", Plays.Scissors }
    };

    int MapPlayToVal(string play, Dictionary<string, Plays> map) => map[play] switch
    {
        Plays.Rock => 1,
        Plays.Paper => 2,
        Plays.Scissors => 3,
        _ => 0
    };

    int returnPlayResult(Plays oppPlay, Plays youPlay)
    {
        if ((oppPlay == Plays.Rock && youPlay == Plays.Rock) || (oppPlay == Plays.Paper && youPlay == Plays.Paper) || (oppPlay == Plays.Scissors && youPlay == Plays.Scissors))
            return 3;

        if ((oppPlay == Plays.Paper && youPlay == Plays.Scissors) || (oppPlay == Plays.Scissors && youPlay == Plays.Rock) || (oppPlay == Plays.Rock && youPlay == Plays.Paper))
            return 6;


        return 0;
    }

    Plays? returnPlay(Plays opp, string me)
    {
        if (me == "X")
        {
            if (opp == Plays.Rock)
                return Plays.Scissors;
            if (opp == Plays.Paper)
                return Plays.Rock;
            if (opp == Plays.Scissors)
                return Plays.Paper;
        }

        if (me == "Y")
        {
            if (opp == Plays.Rock)
                return Plays.Rock;
            if (opp == Plays.Paper)
                return Plays.Paper;
            if (opp == Plays.Scissors)
                return Plays.Scissors;
        }

        if (me == "Z")
        {
            if (opp == Plays.Rock)
                return Plays.Paper;
            if (opp == Plays.Paper)
                return Plays.Scissors;
            if (opp == Plays.Scissors)
                return Plays.Rock;
        }

        return null;
    }

    int getResultByValue(string me)
    {
        if (me == "X")
            return 0;

        if (me == "Y")
            return 3;

        if (me == "Z")
            return 6;

        return 0;
    }
}

enum Plays
{
    Rock,
    Paper,
    Scissors
}