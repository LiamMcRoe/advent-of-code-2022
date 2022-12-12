namespace AdventOfCode.Day12
{
	public static class Day12
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllLines(inputPath);
			var graph = new char[input.Length, input[1].Length];
			var part1Start = (0, 0);
			var part2Start = (0, 0);
			for (int i = 0; i < input.Length; i++)
			{ 
				for (int j = 0; j < input[i].Length; j++)
				{
					var point = input[i][j];
					if (point == 'S') part1Start = (i, j);
					if (point == 'E') part2Start = (i, j);
					graph[i, j] = point;
				}
			}

			var part1 = GetShortestPath(graph, part1Start, x => x == 'E', (a, b) => NormaliseHeight(b) <= NormaliseHeight(a) + 1);
			var part2 = GetShortestPath(graph, part2Start, x => x == 'a' || x == 'S', (a, b) => NormaliseHeight(a) <= NormaliseHeight(b) + 1);

			Console.WriteLine($"Shortest path from origin (part one): {part1}");
			Console.WriteLine($"Shortest path from 'a' node (part two): {part2}");
		}

		private static int GetShortestPath(char[,] graph, (int i, int j) startNode, Func<char, bool> endCondition, Func<char, char, bool> adjacencyTest)
		{
			var maxRowIndex = graph.GetLength(0) - 1;
			var maxColIndex = graph.GetLength(1) - 1;

			List<(int i, int j)> visitedNodes = new() { startNode };
			List<(int i, int j)> nodesToVisit = new() { startNode };
			
			var steps = 0;
			while (nodesToVisit.Any())
			{
				var nextStepNodes = new List<(int i, int j)>();
				foreach (var node in nodesToVisit)
				{
					if (endCondition(graph[node.i, node.j])) return steps;
					var neighbours = GetUnvisitedNeighbours(graph, node, maxRowIndex, maxColIndex, visitedNodes, adjacencyTest);
					visitedNodes.AddRange(neighbours);
					nextStepNodes.AddRange(neighbours);
				}
				nodesToVisit = nextStepNodes;
				steps++;
			}
			return -1;
		}

		private static List<(int i, int j)> GetUnvisitedNeighbours(char[,] graph, (int i, int j) node, int maxRowIndex, int maxColIndex, List<(int i, int j)> visitedNodes, Func<char, char, bool> adjacencyTest) =>
			GetCardinalNeighbours(node)
			.Where(x =>
				NodeInBounds(x, maxRowIndex, maxColIndex)
				&& adjacencyTest(graph[node.i, node.j], graph[x.i, x.j])
				&& !visitedNodes.Contains(x))
			.ToList();

		private static List<(int i, int j)> GetCardinalNeighbours((int i, int j) node) =>
			new() { (node.i - 1, node.j), (node.i + 1, node.j), (node.i, node.j - 1), (node.i, node.j + 1) };

		private static char NormaliseHeight(char original) =>
			original == 'S' ? 'a' :
			original == 'E' ? 'z' :
			original;

		private static bool NodeInBounds((int i, int j) node, int maxRowIndex, int maxColIndex) =>
			0 <= node.i
			&& node.i <= maxRowIndex
			&& 0 <= node.j
			&& node.j <= maxColIndex;
	}
}
