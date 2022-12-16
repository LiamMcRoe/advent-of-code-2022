namespace AdventOfCode.Day16
{
	public static class Day16
	{
		public static void Run(string inputPath)
		{ 
			var valves = File.ReadAllLines(inputPath).Select(x => new Valve(x)).ToDictionary(x => x.ValveCode);

			foreach (var valve in valves.Values) 
			{
				foreach (var code in valve.ConnectedCodes)
				{
					valve.AddConnectedValve(valves[code]);
				}
			}

			var startValve = valves["AA"];
			var score = startValve.CalculateMaxPressueReleased(30, 0, new Dictionary<string, bool>());
			
			Console.WriteLine(score);
		}
	}
}
