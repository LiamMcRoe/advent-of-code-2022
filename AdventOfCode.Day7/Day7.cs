namespace AdventOfCode.Day7
{
	public static class Day7
	{
		public static void Run(string inputPath)
		{
			PartOne(inputPath);
			PartTwo(inputPath);
		}

		private static void PartOne(string inputPath)
		{
			var instructions = File.ReadAllLines(inputPath);
			var fileSystem = new FileSystem(instructions);
			Console.WriteLine($"Total size of directories with size <= 100000: {fileSystem.Root.GetTotalSizeUnderThreshold(100000)}");
		}

		private static void PartTwo(string inputPath)
		{
			var instructions = File.ReadAllLines(inputPath);
			var fileSystem = new FileSystem(instructions);
			Console.WriteLine($"Total size of smallest directory that can be deleted to free enough space: {fileSystem.Root.GetSmallestOverThreshold(fileSystem.GetRequiredFreeSpace(70000000, 30000000))}");
		}
	}
}
