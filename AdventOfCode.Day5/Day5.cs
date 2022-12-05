using System.Text;

namespace AdventOfCode.Day5
{
	public static class Day5
	{
		public static void Run(string inputPath)
		{
			Console.WriteLine($"Part one final positions: {GetFinalCratePositions(inputPath, false)}");
			Console.WriteLine($"Part two final positions: {GetFinalCratePositions(inputPath, true)}");
		}

		private static string GetFinalCratePositions(string inputPath, bool moveMultiple)
		{
			var parser = new InputParser(File.ReadAllLines(inputPath));
			var crates = parser.GetCrateStacks();
			SortCrates(crates, parser.GetInstructions(), moveMultiple);
			var sb = new StringBuilder();
			crates.ForEach(x => sb.Append(x.TryPeek(out var crate) ? crate : ' '));
			return sb.ToString();
		}

		private static void SortCrates(List<Stack<char>> crates, List<(int NumberToMove, int MoveFromIndex, int MoveToIndex)> instructions, bool moveMultiple) =>
			instructions.ForEach(x => ExecuteInstruction(crates, x, moveMultiple));

		private static void ExecuteInstruction(List<Stack<char>> crates, (int NumberToMove, int MoveFromIndex, int MoveToIndex) instruction, bool moveMultiple)
		{
			var leftToMove = instruction.NumberToMove;
			var moveAtOnce = moveMultiple ? instruction.NumberToMove : 1;
			while (leftToMove != 0)
			{
				var cratesToMove = new Stack<char>();
				while (cratesToMove.Count < moveAtOnce)
				{
					cratesToMove.Push(crates[instruction.MoveFromIndex].Pop());
				}
				foreach (var crate in cratesToMove) crates[instruction.MoveToIndex].Push(crate);

				leftToMove -= moveAtOnce;
			}
		}
	}
}
