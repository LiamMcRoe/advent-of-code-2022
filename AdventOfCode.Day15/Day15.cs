using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
			var blockedPoints = new List<Point>();
			foreach (var signal in signals) blockedPoints.AddRange(signal.GetBlockedPointsByRow(2000000));
			blockedPoints.RemoveAll(x => signals.Select(s => s.ClosestBeacon).Contains(x));
			Console.WriteLine(blockedPoints.Distinct().Count());
		}

		private static void PartTwo(List<Signal> signals)
		{
			var beaconLocation = GetDistressBeacon(signals);
			long answer = (beaconLocation.X * 4000000) + beaconLocation.Y;
			Console.WriteLine(answer);
		}

		private static Point GetDistressBeacon(List<Signal> signals)
		{
			for (int y = 0; y <= 4000000; y++)
			{
				var blocked = new List<(long xMin, long xMax)>();
				foreach (var signal in signals)
				{
					var b = signal.GetBlockedInterval(y);
					if (b.HasValue) blocked.Add(b.Value);
				}
				var x = FindUnblockedPoint(blocked, y);
				if (x.HasValue) return x.Value;
			}

			return new Point(0, 0);
		}

		private static Point? FindUnblockedPoint(List<(long xMin, long xMax)> blocked, long y)
		{
			var min = blocked.OrderBy(x => x.xMin).ToArray();
			if (min[0].xMin > 0) return new Point(0, y);

			var lastMax = min.First().xMax;
			for(int i = 1; i < min.Length; i++)
			{
				if ((lastMax < min[i].xMin))
				{
					return new Point(lastMax + 1, y);
				}
				if (min[i].xMax > lastMax) lastMax = min[i].xMax;
			}
			return lastMax < 4000000 ? new Point(4000000, y) : null;
		} 
	}
}
