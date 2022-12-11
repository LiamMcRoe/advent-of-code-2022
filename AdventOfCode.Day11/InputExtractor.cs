using System.Text.RegularExpressions;

namespace AdventOfCode.Day11
{
	public class InputExtractor
	{
		public static Queue<long> ExtractItemQueue(string startingItems)
		{
			var queue = new Queue<long>();
			var itemValues = Regex.Match(startingItems, InputPatterns.StartingItemsPattern).Value.Split(", ");
			foreach (var itemValue in itemValues)
			{
				queue.Enqueue(int.Parse(itemValue));
			}
			return queue;
		}

		public static Func<long, long> ExtractOperation(string operationDefinition)
		{
			var items = Regex.Match(operationDefinition, InputPatterns.OperationPattern).Value.Split(' ');
			var adjustmentValue = items[1];
			return items[0] switch
			{
				"*" => adjustmentValue == "old" ?
					x => x * x :
					x => x * int.Parse(adjustmentValue),
				"+" => adjustmentValue == "old" ?
					x => x * 2 :
					x => x + int.Parse(adjustmentValue),
				_ => throw new InvalidOperationException("Only '+' and '*' operations are supported")
			};
		}

		public static int ExtractIntByPattern(string extractFrom, string matchPattern) => int.Parse(Regex.Match(extractFrom, matchPattern).Value);
	}
}
