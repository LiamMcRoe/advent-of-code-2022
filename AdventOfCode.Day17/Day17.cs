namespace AdventOfCode.Day17
{
	public static class Day17
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllText(inputPath);
			PartOne(input);
			PartTwo(input);
		}

		private static void PartOne(string input)
		{
			var playingSurface = new PlayingSurface(input);
			var part1Height = playingSurface.SimulateBlocks(2022);
			Console.WriteLine($"Height after 2022 iterations (part one): {part1Height}");
		}

		private static void PartTwo(string input)
		{
			var playingSurface = new PlayingSurface(input);
			var part2Height = playingSurface.SimulateBlocks(1000000000000);
			Console.WriteLine($"Height after 1000000000000 iterations (part two): {part2Height}");
		}
	}
}
