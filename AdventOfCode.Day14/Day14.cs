using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;

namespace AdventOfCode.Day14
{
	public static class Day14
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllLines(inputPath);
			PartOne(input);
			PartTwo(input);
		}

		private static void PartOne(string[] input)
		{
			var blockedPoints = MapRocks(input);
			var rockCount = blockedPoints.Count;
			var startingPoint = (500, 0);

			while (!blockedPoints.Contains((-1, -1)))
			{
				var restingPosition = MoveSand(startingPoint, blockedPoints);
				blockedPoints.Add(restingPosition);
			}
			Console.WriteLine($"Sand settled before it falls infinitely (part one) {blockedPoints.Count - rockCount - 1}");
		}

		private static void PartTwo(string[] input) 
		{
			var blockedPoints = MapRocks(input);
			var rockCount = blockedPoints.Count;
			var floorY = blockedPoints.Select(x => x.y).Max() + 2;
			var startingPoint = (500, 0);

			while (!blockedPoints.Contains(startingPoint))
			{
				var restingPosition = MoveSand(startingPoint, blockedPoints, floorY);
				blockedPoints.Add(restingPosition);
			}
			Console.WriteLine($"Sand settled before cave filled (part two) {blockedPoints.Count - rockCount}");
		}

		private static HashSet<(int x, int y)> MapRocks(string[] input)
		{
			var rocks = new List<(int x, int y)>();
			foreach (var pathDef in input)
			{
				rocks.AddRange(MapPath(pathDef));
			}
			return rocks.Distinct().ToHashSet();
		}

		private static HashSet<(int x, int y)> MapPath(string pathDef)
		{
			var path = new HashSet<(int x, int y)>();
			var points = pathDef.Split(" -> ").Select(x=> x.Split(',')).Select(a => (x:int.Parse(a[0]), y:int.Parse(a[1]))).ToArray();
			for (int i = 0; i < points.Length - 1; i++)
			{
				var currentPoint = points[i];
				var endPoint = points[i + 1];
				var xAdd = Math.Sign(endPoint.x - currentPoint.x);
				var yAdd = Math.Sign(endPoint.y - currentPoint.y);
				while (currentPoint != points[i + 1])
				{
					path.Add(currentPoint);
					currentPoint = (currentPoint.x + xAdd, currentPoint.y + yAdd);
				}
			}
			path.Add(points.Last());
			return path;
		}

		private static (int x, int y) MoveSand((int x, int y) sandPoint, HashSet<(int x, int y)> blockedPoints, int? floorY = null)
		{
			while (true)
			{
				if (!floorY.HasValue && !blockedPoints.Any(a => a.y > sandPoint.y)) return (-1, -1);
				if (floorY.HasValue && sandPoint == (sandPoint.x, floorY.Value - 1)) return sandPoint;
				if (!blockedPoints.Contains((sandPoint.x, sandPoint.y + 1))) sandPoint = (sandPoint.x, sandPoint.y + 1);
				else if (!blockedPoints.Contains((sandPoint.x - 1, sandPoint.y + 1))) sandPoint = (sandPoint.x - 1, sandPoint.y + 1);
				else if (!blockedPoints.Contains((sandPoint.x + 1, sandPoint.y + 1))) sandPoint = (sandPoint.x + 1, sandPoint.y + 1);
				else return sandPoint;
			}
		}
	}
}
