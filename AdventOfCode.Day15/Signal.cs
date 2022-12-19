using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day15
{
	public class Signal
	{
		private readonly Point signalLocation;
		public Signal(string signalDefinition)
		{
			var numbers = Regex.Matches(signalDefinition, @"-?\d+").Select(x => int.Parse(x.Value)).ToArray();
			signalLocation = new Point(numbers[0], numbers[1]);
			ClosestBeacon = new Point(numbers[2], numbers[3]);
		}

		public Point ClosestBeacon { get; init; }

		public long Distance => Math.Abs(signalLocation.X - ClosestBeacon.X) + Math.Abs(signalLocation.Y - ClosestBeacon.Y);

		public List<Point> GetBlockedPointsByRow(int y)
		{
			if ((signalLocation.Y < y && signalLocation.Y + Distance < y) || signalLocation.Y > y && signalLocation.Y - Distance > y)
				return new List<Point>();

			var x1 = Distance - Math.Abs(signalLocation.Y - y) + signalLocation.X;
			var x2 = signalLocation.X + Math.Abs(signalLocation.Y - y) - Distance;

			if (x1 <= x2) return Enumerable.Range((int)x1, (int)(x2 - x1 + 1)).Select(x => new Point(x, y)).ToList();
			return Enumerable.Range((int)x2, (int)(x1 - x2 + 1)).Select(x => new Point(x, y)).ToList();
		}

		public (long xMin, long xMax)? GetBlockedInterval(int y)
		{
			if ((signalLocation.Y < y && signalLocation.Y + Distance < y) || signalLocation.Y > y && signalLocation.Y - Distance > y)
				return null;

			var x1 = Distance - Math.Abs(signalLocation.Y - y) + signalLocation.X;
			var x2 = signalLocation.X + Math.Abs(signalLocation.Y - y) - Distance;

			return x2 < x1 ? (x2, x1) : (x1, x2);
		}
	}
}
