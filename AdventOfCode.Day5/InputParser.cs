using System.Text;
using System.Text.RegularExpressions;

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
		}

		public List<Stack<char>> GetCrateStacks() => ParseCratePositions(cratePositions).Select(x => new Stack<char>(x)).ToList();

		public List<(int NumberToMove, int MoveFromIndex, int MoveToIndex)> GetInstructions() => instructions.Select(x => ParseInstructionLine(x)).ToList();

		private static List<string> ParseCratePositions(string[] cratePositions)
		{
			var rows = cratePositions.Length;
			var cols = cratePositions.Max(x => x.Length);

			// Transpose so that each string defines a single stack of crates.
			var result = new List<string>();
			for (int i = 0; i < cols; i++)
			{
				var sb = new StringBuilder();
				for (int j = rows - 1; j >= 0; j--)
				{
					// Only care about including letters since these are our crates.
					if (char.IsLetter(cratePositions[j][i])) sb.Append(cratePositions[j][i]);
				}
				var stackString = sb.ToString();
				if (!string.IsNullOrEmpty(stackString)) result.Add(stackString);
			}
			return result;
		}

		private static (int NumberToMove, int MoveFromIndex, int MoveToIndex) ParseInstructionLine(string instructionLine)
		{
			var numbers = Regex.Matches(instructionLine, @"\d+").Select(x => int.Parse(x.Value)).ToArray();
			return (NumberToMove: numbers[0], MoveFromIndex: numbers[1] - 1, MoveToIndex: numbers[2] - 1);
		}
	}
}
