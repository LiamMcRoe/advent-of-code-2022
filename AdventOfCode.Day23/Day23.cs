namespace AdventOfCode.Day23
{
    public static class Day23
    {
        public static void Run(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);
            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(string[] input)
        {
            var grid = new Grid(input);
            var emptyTiles = grid.SimulateRounds(10);
            Console.WriteLine($"Empty tiles after 10 rounds (part one): {emptyTiles}");
        }

        private static void PartTwo(string[] input)
        {
            var grid = new Grid(input);
            var roundNumber = 1;
            while (grid.SimulateRound()) roundNumber++;
            Console.WriteLine($"First round with no movement (part two): {roundNumber}");
        }
    }
}
