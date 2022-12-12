namespace AdventOfCode.Day12
{
	public static class Day12
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllLines(inputPath);
			var graph = new char[input.Length, input[1].Length];
			NodePosition part1Start = new (0, 0);
			NodePosition part2Start = new (0, 0);
			for (int i = 0; i < input.Length; i++)
			{
				for (int j = 0; j < input[i].Length; j++)
				{
					var point = input[i][j];
					if (point == 'S') part1Start = new (i, j);
					if (point == 'E') part2Start = new (i, j);
					graph[i, j] = point;
				}
			}

			var part1 = GetShortestPathLength(graph, part1Start, x => x == 'E', (a, b) => NormaliseHeight(b) <= NormaliseHeight(a) + 1);
			var part2 = GetShortestPathLength(graph, part2Start, x => x == 'a' || x == 'S', (a, b) => NormaliseHeight(a) <= NormaliseHeight(b) + 1);

			Console.WriteLine($"Shortest path from origin (part one): {part1}");
			Console.WriteLine($"Shortest path from 'a' node (part two): {part2}");
		}

		private static int GetShortestPathLength(char[,] graph, NodePosition startPosition, Func<char, bool> endCondition, Func<char, char, bool> adjacencyTest)
		{
			var maxRowIndex = graph.GetLength(0) - 1;
			var maxColIndex = graph.GetLength(1) - 1;

			List<NodePosition> visitedNodes = new() { startPosition };
			Queue<(NodePosition node, int pathLength)> nodesToVisit = new();
			nodesToVisit.Enqueue((startPosition, 0));
			
			while (nodesToVisit.Any())
			{
				var (nodePosition, pathLength) = nodesToVisit.Dequeue();
				if (endCondition(graph.GetValue(nodePosition))) return pathLength;
				var neighbours = GetUnvisitedNeighbours(graph, nodePosition, maxRowIndex, maxColIndex, visitedNodes, adjacencyTest);

				foreach (var neighbour in neighbours)
				{
					visitedNodes.Add(neighbour);
					nodesToVisit.Enqueue((neighbour, pathLength + 1));
				}
			}
			return -1;
		}

		private static List<NodePosition> GetUnvisitedNeighbours(char[,] graph, NodePosition nodePosition, int maxRowIndex, int maxColIndex, List<NodePosition> visitedNodes, Func<char, char, bool> adjacencyTest) =>
			GetCardinalNeighbours(nodePosition)
			.Where(x =>
				x.IsInBounds(maxRowIndex, maxColIndex)
				&& adjacencyTest(graph.GetValue(nodePosition), graph.GetValue(x))
				&& !visitedNodes.Contains(x))
			.ToList();

		private static List<NodePosition> GetCardinalNeighbours(NodePosition nodePosition) =>
			new() { new (nodePosition.IPosition - 1, nodePosition.JPosition), new (nodePosition.IPosition + 1, nodePosition.JPosition), new (nodePosition.IPosition, nodePosition.JPosition - 1), new (nodePosition.IPosition, nodePosition.JPosition + 1) };

		private static char NormaliseHeight(char original) =>
			original == 'S' ? 'a' :
			original == 'E' ? 'z' :
			original;

		private static T GetValue<T>(this T[,] matrix, NodePosition nodePosition) => matrix[nodePosition.IPosition, nodePosition.JPosition];
	}
}
