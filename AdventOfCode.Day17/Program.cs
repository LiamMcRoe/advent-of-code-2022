
using AdventOfCode.Day17;

var input = File.ReadAllText("Input.txt");

var playingSurface = new PlayingSurface(input);
var part1Height = playingSurface.SimulateBlocks(2022);
Console.WriteLine($"Height after 2022 iterations (part one): {part1Height}");

playingSurface = new PlayingSurface(input);
var part2Height = playingSurface.SimulateBlocks(1000000000000);
Console.WriteLine($"Height after 1000000000000 iterations (part two): {part2Height}");