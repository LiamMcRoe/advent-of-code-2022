namespace AdventOfCode.Day21
{
	public static class Day21
	{
		public static void Run(string inputPath)
		{
			var allMonkeys = File.ReadAllLines(inputPath).Select(x => x.Split(':')).ToDictionary(x => x[0], x => x[1].Trim());
			PartOne(allMonkeys);
			PartTwo(allMonkeys);
		}

		private static void PartOne(Dictionary<string, string> allMonkeys)
		{
			var root = allMonkeys["root"];
			var rootNumber = GetNumber(root, allMonkeys);
			Console.WriteLine($"Number yelled by root (part one): {rootNumber}");
		}

		private static void PartTwo(Dictionary<string, string> allMonkeys)
		{
			var root = allMonkeys["root"];
			var command = root.Split(" + ");
			var lhsContainsHuman = ContainsHuman(allMonkeys[command[0]], allMonkeys);
			var scoreWithoutHuman = GetNumber(allMonkeys[command[lhsContainsHuman ? 1 : 0]], allMonkeys);

			long l = 0;
			long r = long.MaxValue;
			long m = 0;
			double scoreWithHuman = 0;
			while (scoreWithHuman != scoreWithoutHuman)
			{
				m = (l + r) / 2;
				allMonkeys["humn"] = m.ToString();
				scoreWithHuman = GetNumber(allMonkeys[command[lhsContainsHuman ? 0 : 1]], allMonkeys);
				if (scoreWithHuman > scoreWithoutHuman) l = m + 1;
				else if (scoreWithHuman < scoreWithoutHuman) r = m - 1;
			}
			Console.WriteLine($"Number yelled to make root's sides equal (part two): {m}");
		}

		private static double GetNumber(string monkeyOperation, Dictionary<string, string> allMonkeys)
		{
			if (double.TryParse(monkeyOperation, out var number)) return number;

			var operationSymbol = monkeyOperation[4..7];
			return operationSymbol switch
			{
				" + " => HandleCommand(monkeyOperation.Split(" + "), allMonkeys, (x, y) => x + y),
				" - " => HandleCommand(monkeyOperation.Split(" - "), allMonkeys, (x, y) => x - y),
				" * " => HandleCommand(monkeyOperation.Split(" * "), allMonkeys, (x, y) => x * y),
				" / " => HandleCommand(monkeyOperation.Split(" / "), allMonkeys, (x, y) => x / y),
				_ => throw new InvalidOperationException()
			};
		}

		private static bool ContainsHuman(string monkeyOperation, Dictionary<string, string> allMonkeys)
		{
			if (int.TryParse(monkeyOperation, out _)) return false;
			if (monkeyOperation.Contains("humn")) return true;
			var lhs = monkeyOperation[..4];
			var rhs = monkeyOperation[(monkeyOperation.Length - 4)..];
			return ContainsHuman(allMonkeys[lhs], allMonkeys) || ContainsHuman(allMonkeys[rhs], allMonkeys);
		}

		private static double HandleCommand(string[] splitCommand, Dictionary<string, string> allMonkeys, Func<double, double, double> operation) =>
			operation(GetNumber(allMonkeys[splitCommand[0]], allMonkeys), GetNumber(allMonkeys[splitCommand[1]], allMonkeys));

	}
}
