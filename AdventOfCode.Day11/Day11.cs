using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11
{
	public static class Day11
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllText(inputPath);
			var monkeyDefinitions = input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			var part1 = GetMonkeyGroup(monkeyDefinitions).ExecuteRounds(20, true).CalculateMonkeyBusiness(2);
			Console.WriteLine($"Monkey business after 20 rounds with worry level adjustment (part one): {part1}");

			var part2 = GetMonkeyGroup(monkeyDefinitions).ExecuteRounds(10000, false).CalculateMonkeyBusiness(2);
			Console.WriteLine($"Monkey business after 10000 rounds without worry level adjustment (part two): {part2}");
		}

		public static MonkeyGroup GetMonkeyGroup(string[] monkeyDefinitions)
		{
			var monkeyGroup = new MonkeyGroup();
			foreach (var monkeyDefinition in monkeyDefinitions) monkeyGroup.AddMonkey(monkeyDefinition);
			return monkeyGroup;
		}
	}
}
