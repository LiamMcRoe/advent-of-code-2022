namespace AdventOfCode.Day11
{
	public class InputPatterns
	{
		public const string MonkeyIndexPattern = @"\d+";
		public const string StartingItemsPattern = @"(?<=  Starting items: ).*";
		public const string OperationPattern = @"(?<=  Operation: new = old ).*";
		public const string TestDivisorPattern = @"(?<=  Test: divisible by ).*";
		public const string TestPassPattern = @"(?<=    If true: throw to monkey ).*";
		public const string TestFailPattern = @"(?<=    If false: throw to monkey ).*";
	}
}
