using System.Drawing;

namespace AdventOfCode.Day9
{
	public static class Day9
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllLines(inputPath);

			Console.WriteLine($"Part one: {GetNumberOfVisitedPoints(input, 2)}");
			Console.WriteLine($"Part two: {GetNumberOfVisitedPoints(input, 10)}");
		}

		private static int GetNumberOfVisitedPoints(string[] instructions, int numberKnots)
		{
			List<Point> currentPoints = Enumerable.Repeat(new Point(0, 0), numberKnots).ToList();
			var visitedPoints = new List<Point>() { currentPoints.Last() };
			foreach (var instruction in instructions)
			{
				currentPoints = ProcessInstruction(currentPoints, visitedPoints, instruction);
			}
			return visitedPoints.Count;
		}

		private static List<Point> ProcessInstruction(List<Point> currentPoints, List<Point> visitedPoints, string instruction)
		{
			var decomposedInstruction = instruction.Split(' ');
			var numMoves = int.Parse(decomposedInstruction[1]);
			for (int i = 1; i <= numMoves; i++) 
			{
				currentPoints = ProcessSingleMove(currentPoints, visitedPoints, decomposedInstruction[0]);
			}
			return currentPoints;
		}

		private static List<Point> ProcessSingleMove(List<Point> currentPoints, List<Point> visitedPoints, string direction)
		{
			currentPoints[0] = GetNewHeadPosition(currentPoints[0], direction);

			for (int i = 1; i < currentPoints.Count; i++)
			{
				currentPoints[i] = GetNewFollowerPosition(currentPoints[i - 1], currentPoints[i]);
			}

			if (!visitedPoints.Contains(currentPoints.Last())) visitedPoints.Add(currentPoints.Last());
			return currentPoints;
		}

		private static Point GetNewHeadPosition(Point head, string direction) => direction switch
		{
			"U" => new Point(head.X, head.Y + 1),
			"D" => new Point(head.X, head.Y - 1),
			"L" => new Point(head.X - 1, head.Y),
			"R" => new Point(head.X + 1, head.Y),
			_ => throw new InvalidOperationException()
		};

		private static Point GetNewFollowerPosition(Point leader, Point follower)
		{
			if (AreTouching(leader, follower)) return follower;
			if (leader.X == follower.X) return new Point(follower.X, follower.Y + Math.Sign(leader.Y - follower.Y));
			if (leader.Y == follower.Y) return new Point(follower.X + Math.Sign(leader.X - follower.X), follower.Y);
			return new Point(follower.X + Math.Sign(leader.X - follower.X), follower.Y + Math.Sign(leader.Y - follower.Y));
		}

		private static bool AreTouching(Point leader, Point follower) => (Math.Abs(leader.X - follower.X) <= 1 && Math.Abs(leader.Y - follower.Y) <= 1);
	}
}
