using System.Text;

namespace AdventOfCode.Day5
{
	public static class Day5
	{
		public static void Run(string inputPath)
		{
			Console.WriteLine($"Part one final positions: {GetFinalPositions(inputPath, false)}");
			Console.WriteLine($"Part two final positions: {GetFinalPositions(inputPath, true)}");
		}

		private static string GetFinalPositions(string inputPath, bool moveMultiple)
		{
			var parser = new InputParser(File.ReadAllLines(inputPath));
			var crates = parser.GetCrateStacks();
			SortCrates(crates, parser.GetInstructions(), moveMultiple);
			var sb = new StringBuilder();
			foreach (var crateStack in crates) sb.Append(crateStack.TryPeek(out var crate) ? crate : ' ');
			return sb.ToString();
		}

		private static void SortCrates(List<Stack<char>> crates, List<(int NumberToMove, int MoveFromIndex, int MoveToIndex)> instructions, bool moveMultiple) 
		{
			foreach (var instruction in instructions)
			{
				ExecuteInstruction(crates, instruction, moveMultiple);
			}
		}

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
