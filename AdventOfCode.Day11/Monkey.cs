namespace AdventOfCode.Day11
{
	public class Monkey
	{
		private readonly Queue<long> items;
		private readonly Func<long, long> operation;
		private readonly int onPassedTest;
		private readonly int onFailedTest;

		public Monkey(string[] monkeyDefinition)
		{
			Index = InputExtractor.ExtractIntByPattern(monkeyDefinition[0], InputPatterns.MonkeyIndexPattern);
			items = InputExtractor.ExtractItemQueue(monkeyDefinition[1]);
			operation = InputExtractor.ExtractOperation(monkeyDefinition[2]);
			TestDivisor = InputExtractor.ExtractIntByPattern(monkeyDefinition[3], InputPatterns.TestDivisorPattern);
			onPassedTest = InputExtractor.ExtractIntByPattern(monkeyDefinition[4], InputPatterns.TestPassPattern);
			onFailedTest =  InputExtractor.ExtractIntByPattern(monkeyDefinition[5], InputPatterns.TestFailPattern);
		}

		public int Index { get; init; }
		public bool HasItem => items.Count > 0;
		public long ItemsInspected { get; private set; }
		public int TestDivisor { get; init; }

		public (long Item, int PassToIndex) PerformInspection(bool adjustWorryLevel, long worryReducingMod)
		{
			var itemWorryLevel = items.Dequeue();
			itemWorryLevel = operation(itemWorryLevel);
			if (adjustWorryLevel) itemWorryLevel = AdjustWorryLevel(itemWorryLevel);
			var passToIndex = TestWorryLevel(itemWorryLevel);
			itemWorryLevel %= worryReducingMod;
			ItemsInspected++;
			return (itemWorryLevel, passToIndex);
		}

		public void EnqueueItem(long item) => items.Enqueue(item);

		private int TestWorryLevel(long worryLevel) => worryLevel % TestDivisor == 0 ? onPassedTest : onFailedTest;

		private static long AdjustWorryLevel(long worryLevel) => worryLevel / 3;
	}
}
