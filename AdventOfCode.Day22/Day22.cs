namespace AdventOfCode.Day22
{
	public static class Day22
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllText(inputPath);
			var splitInput = input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			var map = new Map(splitInput[0].Split(Environment.NewLine));
			var password = map.CalculatePassword(splitInput[1]);

			Console.WriteLine(password);
		}
	}
}
