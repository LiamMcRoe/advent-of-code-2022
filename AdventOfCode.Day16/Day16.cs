namespace AdventOfCode.Day16
{
	public static class Day16
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllLines(inputPath);

			PartOne(input);
			PartTwo(input);
		}
			
		private static void PartOne(string[] input)
		{
			var valves = GetValves(input);
			var startValve = valves["AA"];
			var score = startValve.CalculateMaxPressueReleased(30, 0, new HashSet<string>());

			Console.WriteLine(score);
		}

		private static void PartTwo(string[] input)
		{
			var valves = GetValves(input);
			var startValve = valves["AA"];
			var score = startValve.CalculateMaxPressueReleased(26, 0, new HashSet<string>()); // Tracking of open valves wrong, just reporting all are open

			var openedInFirstPass = valves.Select(x => x.Value.knownStates.Where(a => a.Key.Contains("[2]")).OrderByDescending(a => a.Value).First()).OrderByDescending(a => a.Value).First().Key.Replace("[2]", "").Split(",").ToHashSet();
			valves = GetValves(input);
			startValve = valves["AA"];
			score += startValve.CalculateMaxPressueReleased(26, 0, openedInFirstPass);

			Console.WriteLine(score);
		}

		private static Dictionary<string, Valve> GetValves(string[] input)
		{ 
			var valves = input.Select(x => new Valve(x)).ToDictionary(x => x.ValveCode);

			foreach (var valve in valves.Values)
			{
				foreach (var code in valve.ConnectedCodes)
				{
					valve.AddConnectedValve(valves[code]);
				}
			}
			return valves;
		}
	}
}
