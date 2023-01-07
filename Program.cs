// See https://aka.ms/new-console-template for more information
using adventofcode2022;
using System.Diagnostics;

Console.WriteLine("Started");

var timer = new Stopwatch();
timer.Start();

var totalScore = await DayTwelve.Part2();

// var dayOne = new DayOne();
// var maxCaloriesFromElves = await dayOne.GetMaxCaloriesFromElves();
// var maxTopThreeCaloriesFromElves = await dayOne.GetTopThreeMaxCaloriesFromElves();

timer.Stop();

Console.WriteLine($"Total score: {totalScore}");
// Console.WriteLine($"Elve max calorie: {maxCaloriesFromElves}");
// Console.WriteLine($"Elves top three max calories sum: {maxTopThreeCaloriesFromElves}");
Console.WriteLine($"Took {timer.Elapsed}");