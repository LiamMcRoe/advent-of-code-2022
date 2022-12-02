namespace AdventOfCode.DayTwo
{
    public static class DayTwo
    {
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
                currentScore += GetRoundScore(line, guideExplained);
            }
            return currentScore;
        }

        private static int GetRoundScore(string roundDef, bool guideExplained)
        {
			(char playerOne, char playerTwo) round = (roundDef[0], roundDef[2]);

            return round switch
			{
				('A', 'X') => guideExplained ? 3 : 4,
				('A', 'Y') => guideExplained ? 4 : 8,
				('A', 'Z') => guideExplained ? 8 : 3,
				('B', 'X') => guideExplained ? 1 : 1,
				('B', 'Y') => guideExplained ? 5 : 5,
				('B', 'Z') => guideExplained ? 9 : 9,
				('C', 'X') => guideExplained ? 2 : 7,
				('C', 'Y') => guideExplained ? 6 : 2,
				('C', 'Z') => guideExplained ? 7 : 6,
				_ => throw new NotImplementedException()
			};
        }
	}
}
