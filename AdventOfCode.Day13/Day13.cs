namespace AdventOfCode.Day13
{
	public static class Day13
	{
		public static void Run(string inputPath)
		{
			var part1 = PartOne(inputPath);
			Console.WriteLine($"Sum of indices of correctly sorted packets (part one): {part1}");

			var part2 = PartTwo(inputPath);
			Console.WriteLine($"Product of indices of divider packets (part two): {part2}");
		}

		private static int PartOne(string inputPath)
		{
			var input = File.ReadAllText(inputPath);
			var packetPairDefs = input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			var indexSum = 0;
			for (int i = 0; i < packetPairDefs.Length; i++)
			{
				var packets = packetPairDefs[i].Split(Environment.NewLine);
				if (new Packet(packets[0]).IsLessThan(new Packet(packets[1]))) indexSum += i + 1;
			}
			return indexSum;
		}

		private static int PartTwo(string inputPath)
		{
			var input = File.ReadAllLines(inputPath).Where(x => x != "").ToList();
			input.Add("[[2]]");
			input.Add("[[6]]");
			input.Sort((x, y) => new Packet(x).IsLessThan(new Packet(y)) ? -1 : 1);

			var i = input.IndexOf("[[2]]") + 1;
			var j = input.IndexOf("[[6]]") + 1;

			return i * j;
		}
	}
}
