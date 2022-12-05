using System.Text;

namespace AdventOfCode.Day5
{
	public class InputParser
	{
		private readonly string[] cratePositions;
		private readonly string[] instructions;

		public InputParser(string[] input)
		{
			var divideIndex = Array.IndexOf(input, string.Empty);
			this.cratePositions = input[..(divideIndex - 1)];
			this.instructions = input[(divideIndex + 1)..];
			foreach(var c in cratePositions) Console.WriteLine(c);
		}

		public List<Stack<char>> GetCrateStacks()
		{
			var stacks = new List<Stack<char>>();
			for (int i = cratePositions.Length - 1; i >= 0; i--)
			{ 
				var parsedLine = ParseCrateLine(cratePositions[i]);
				for (int j = 0; j <= parsedLine.Length - 1; j++) 
				{
					if (stacks.Count <= j) stacks.Add(new Stack<char>());
					if (parsedLine[j] != ' ') stacks[j].Push(parsedLine[j]);
				}
			}
			return stacks;
		}

		public List<(int NumberToMove, int MoveFromIndex, int MoveToIndex)> GetInstructions()
		{
			var parsedInstructions = new List<(int NumberToMove, int MoveFromIndex, int MoveToIndex)>();
			foreach (var instruction in instructions)
			{
				var splitInstruction = instruction.Split(' ');
				parsedInstructions.Add((int.Parse(splitInstruction[1]), int.Parse(splitInstruction[3]) - 1, int.Parse(splitInstruction[5]) - 1));
			}
			return parsedInstructions;
		}

		private string ParseCrateLine(string crateLine)
		{
			var parsedLine = new StringBuilder();
			for (int i = 1; i <= crateLine.Length - 1; i += 4) parsedLine.Append(crateLine[i]);
			return parsedLine.ToString();
		}
	}
}
