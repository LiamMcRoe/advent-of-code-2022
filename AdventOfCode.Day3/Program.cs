// See https://aka.ms/new-console-template for more information
using AdventOfCode.Day3;

var backpackGroup = new BackpackGroup(File.ReadAllLines("Input.txt"));
Console.WriteLine($"Part one score: {backpackGroup.GetTotalRepeatedItemsPriority()}");
Console.WriteLine($"Part two score: {backpackGroup.GetTotalBadgePriority()}");
