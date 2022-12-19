namespace AdventOfCode.Day18
{
	public static class Day18
	{
		public static void Run(string inputPath)
		{
			var allCubes = File.ReadAllLines(inputPath).Select(x => x.Split(',')).Select(x => new Cube(int.Parse(x[0]), int.Parse(x[1]), int.Parse(x[2]))).ToHashSet();
			PartOne(allCubes);
			PartTwo(allCubes);
		}

		private static void PartOne(HashSet<Cube> allCubes)
		{
			var unblockedSides = 0;
			foreach (var cube in allCubes) unblockedSides += CountConnections(cube, allCubes, true);
			Console.WriteLine($"Total sides unblocked by others (part one): {unblockedSides}");
		}

		private static void PartTwo(HashSet<Cube> allCubes)
		{
			var openToOutside = GetOpenToOutside(allCubes);
			Console.WriteLine($"Unblocked sides not including air pockets (part two): {openToOutside}");
		}

		private static int GetOpenToOutside(HashSet<Cube> allCubes)
		{
			var minX = allCubes.Min(x => x.X) - 1;
			var maxX = allCubes.Max(x => x.X) + 1;
			var minY = allCubes.Min(x => x.Y) - 1;
			var maxY = allCubes.Max(x => x.Y) + 1;
			var minZ = allCubes.Min(x => x.Z) - 1;
			var maxZ = allCubes.Max(x => x.Z) + 1;

			var start = new Cube(minX, minY, minZ);
			return CountExposedSides(start, new HashSet<Cube>() { start }, allCubes, minX, maxX, minY, maxY, minZ, maxZ);
		}

		private static int CountExposedSides(Cube startPoint, HashSet<Cube> visited, HashSet<Cube> allCubes, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
		{
			var sidesTouched = CountConnections(startPoint, allCubes, false);
			var nextPoints = GetAdjacentCubes(startPoint).Where(p => InBounds(p, minX, maxX, minY, maxY, minZ, maxZ) && !visited.Contains(p) && !allCubes.Contains(p)).ToArray();

			foreach (var cube in nextPoints) visited.Add(cube);
			foreach (var cube in nextPoints) sidesTouched += CountExposedSides(cube, visited, allCubes, minX, maxX, minY, maxY, minZ, maxZ);
			return sidesTouched;
		}

		private static bool InBounds(Cube point, int minX, int maxX, int minY, int maxY, int minZ, int maxZ) =>
			InInterval(point.X, minX, maxX) && InInterval(point.Y, minY, maxY) && InInterval(point.Z, minZ, maxZ);

		private static bool InInterval(int value, int lowerBound, int upperBound) =>
			lowerBound <= value && value <= upperBound;

		private static int CountConnections(Cube cube, HashSet<Cube> allCubes, bool countUnconnected)
		{
			var connectedSides = GetAdjacentCubes(cube).Where(c => allCubes.Contains(c)).Count();
			return countUnconnected ? 6 - connectedSides : connectedSides;
		}

		private static List<Cube> GetAdjacentCubes(Cube cube) => new()
		{
			new Cube(cube.X + 1, cube.Y, cube.Z),
			new Cube(cube.X - 1, cube.Y, cube.Z),
			new Cube(cube.X, cube.Y + 1, cube.Z),
			new Cube(cube.X, cube.Y - 1, cube.Z),
			new Cube(cube.X, cube.Y, cube.Z + 1),
			new Cube(cube.X, cube.Y, cube.Z - 1)
		};
	}
}
