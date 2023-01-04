namespace AdventOfCode.Day23
{
    public static class Day23
    {
        public static void Run(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);
            var grid = new Grid(input);
            var groundCovered = grid.SimulateRounds(int.MaxValue);
            Console.WriteLine(groundCovered);
        }
    }
}
