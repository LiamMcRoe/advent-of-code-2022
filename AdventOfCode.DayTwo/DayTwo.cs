namespace AdventOfCode.DayTwo
{
	public static class DayTwo
	{
		private static readonly Dictionary<string, int> partOneScores = new()
		{
			["A X"] = 4,
			["A Y"] = 8,
			["A Z"] = 3,
			["B X"] = 1,
			["B Y"] = 5,
			["B Z"] = 9,
			["C X"] = 7,
			["C Y"] = 2,
			["C Z"] = 6,
		};

		private static readonly Dictionary<string, int> partTwoScores = new()
		{
			["A X"] = 3,
			["A Y"] = 4,
			["A Z"] = 8,
			["B X"] = 1,
			["B Y"] = 5,
			["B Z"] = 9,
			["C X"] = 2,
			["C Y"] = 6,
			["C Z"] = 7,
		};

		public static void Run(string inputPath)
		{
			Console.WriteLine($"Before guide explanied fully (part one): {GetGameScore(inputPath, false)}");
			Console.WriteLine($"After guide explanied fully (part two): {GetGameScore(inputPath, true)}");
		}

		private static int GetGameScore(string inputPath, bool guideExplained)
		{
			using var sr = File.OpenText(inputPath);
			string? line;
			int currentScore = 0;
			while ((line = sr.ReadLine()) != null)
			{
				currentScore += guideExplained ? partTwoScores[line] : partOneScores[line];
			}
			return currentScore;
		}
	}
}
