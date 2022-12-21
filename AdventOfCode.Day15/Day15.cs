namespace AdventOfCode.Day15
{
	public class Day15
	{
		public static void Run(string inputPath)
		{
			var signals = File.ReadAllLines(inputPath).Select(x => new Signal(x)).ToList();
			PartOne(signals);
			PartTwo(signals);
		}

		private static void PartOne(List<Signal> signals)
		{
			var blockedIntervals = signals.Select(x => x.GetBlockedInterval(2000000)).Where(x => x.HasValue).Select(x => x!.Value).OrderBy(x => x.xMin).ToList();
			var mergedIntervals = MergeIntervals(blockedIntervals);
			long blockedPoints = 0;
			foreach (var (xMin, xMax) in mergedIntervals)
			{
				blockedPoints += xMax - xMin;		
			}
			Console.WriteLine($"Blocked points in row 2000000 (part one): {blockedPoints}");
		}

		private static void PartTwo(List<Signal> signals)
		{
			var beaconLocation = GetDistressBeacon(signals);
			long signalStrength = (beaconLocation.X * 4000000) + beaconLocation.Y;
			Console.WriteLine($"Tuning frequency from distress signal (part two): {signalStrength}");
		}

		private static Point GetDistressBeacon(List<Signal> signals)
		{
			var y = 0;
			while (true)
			{
				var blockedIntervals = new List<(long xMin, long xMax)>();
				foreach (var signal in signals)
				{
					var b = signal.GetBlockedInterval(y);
					if (b.HasValue) blockedIntervals.Add(b.Value);
				}
				var mergedIntervals = MergeIntervals(blockedIntervals);
				if (mergedIntervals.Count > 1) return new Point(mergedIntervals[0].xMax + 1, y);
				if (mergedIntervals.Min(x => x.xMin) > 0) return new Point(0, y);
				if (mergedIntervals.Max(x => x.xMax < 4000000)) return new Point(4000000, y);
				y++;
			}
		}

		private static List<(long xMin, long xMax)> MergeIntervals(List<(long xMin, long xMax)> intervals)
		{
			var mergedIntervals = new List<(long xMin, long xMax)>();
			intervals.Sort();
			var current = intervals[0];
			for (int i = 1; i < intervals.Count; i++)
			{
				if (current.xMax < intervals[i].xMin)
				{
					mergedIntervals.Add(current);
					current = intervals[i];
				}
				else
				{
					current = (current.xMin, Math.Max(current.xMax, intervals[i].xMax));
				}
			}
			mergedIntervals.Add(current);
			return mergedIntervals;
		}
	}
}
