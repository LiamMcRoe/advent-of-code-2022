using System;
using System.Collections.Generic;
using System.Drawing;
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
			BigInteger x = 2655411;
			BigInteger y = 3166538;
			BigInteger r = (x * 4000000) + y;

			Console.WriteLine(r);
			var signals = File.ReadAllLines(inputPath).Select(x => new Signal(x)).ToList();
			//var blockedPoints = new List<Point>();
			//foreach (var signal in signals) blockedPoints.AddRange(signal.GetBlockedPointsByRow(2000000));
			//blockedPoints.RemoveAll(x => signals.Select(s => s.ClosestBeacon).Contains(x));
			//Console.WriteLine(blockedPoints.Distinct().Count());

			var point = GetDistressBeacon(signals);

			Console.WriteLine((point.X * 4000000) + point.Y);
			

		}

		private static Point GetDistressBeacon(List<Signal> signals)
		{
			var xVals = Enumerable.Range(0, 4000001);

			for (int y = 0; y <= 4000000; y++)
			{
				var blocked = new List<(int xMin, int xMax)>();
				foreach (var signal in signals)
				{
					var b = signal.GetBlockedInterval(y);
					if (b.HasValue) blocked.Add(b.Value);
					
				}
				if (AllBlocked(blocked)) continue;
				var x = xVals.Where(x => !blocked.Any(b => b.xMin <= x && x <= b.xMax));
				if (x.Any()) 
					return new Point(x.First(), y);
				
			}

			return new Point(0, 0);
		}

		private static bool AllBlocked(List<(int xMin, int xMax)> blocked)
		{
			// This should probably return the x since it is doing all the work to find it anyway.
			var min = blocked.OrderBy(x => x.xMin).ToArray();
			if (!min.Any(x => x.xMin <= 0))
				return false;

			var lastMax = min.First().xMax;
			for(int i = 1; i < min.Count(); i++)
			{
				if ((lastMax < min[i].xMin))
				{
					return false;
				}
				if (min[i].xMax > lastMax) lastMax = min[i].xMax;
			}
			if (!min.Any(x => x.xMax >= 4000000))
				return false;

			return true;
			
		}
	}
}
