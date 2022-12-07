namespace AdventOfCode.Day7
{
	public class Node
	{
		private int localSize;
		private readonly Dictionary<string, Node> children;

		public Node(Node? parent = null) 
		{
			this.Parent = parent;
			this.children = new Dictionary<string, Node>();
		}

		public Node? Parent { get; set; }

		public int GetSize() => children.Values.Select(x => x.GetSize()).Sum() + localSize;

		public Node AddChild(string name, Node node)
		{
			if (!children.TryGetValue(name, out _)) children[name] = node;
			return this;
		}

		public Node GetChild(string name) => children.TryGetValue(name, out var child) ? child : throw new InvalidOperationException($"Child node with name {name} does not exist.");
		
		public Node IncreaseSize(int increaseBy)
		{
			localSize += increaseBy;
			return this;
		}

		public int GetTotalSizeUnderThreshold(int threshold)
		{
			var mySize = GetSize();
			return children.Values.Select(x => x.GetTotalSizeUnderThreshold(threshold)).Sum() + (mySize <= threshold ? mySize : 0);
		}

		public int GetSmallestOverThreshold(int threshold)
		{
			var currentSize = GetSize();
			var bigEnoughNodes = children.Values.Where(x => x.GetSize() >= threshold);
			foreach (var node in bigEnoughNodes)
			{
				var nodeSmallestOverLimit = node.GetSmallestOverThreshold(threshold);
				if (nodeSmallestOverLimit < currentSize) currentSize = nodeSmallestOverLimit;
			}
			return currentSize;
		}
	}
}
