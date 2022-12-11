namespace AdventOfCode.Day11
{
	public class MonkeyGroup
	{
		private readonly List<Monkey> monkeys;
		private long worryReducingMod;

		public MonkeyGroup()
		{
			this.monkeys = new List<Monkey>();
			this.worryReducingMod = 1;
		}

		public void AddMonkey(string monkeyDefinition) => AddMonkey(new Monkey(monkeyDefinition.Split(Environment.NewLine)));

		public MonkeyGroup AddMonkey(Monkey monkey)
		{
			monkeys.Add(monkey);
			worryReducingMod *= monkey.TestDivisor;
			return this;
		}

		public MonkeyGroup ExecuteRounds(int numberOfRounds, bool adjustWorryLevel)
		{
			while (numberOfRounds > 0)
			{
				foreach (var monkey in monkeys)
				{
					while (monkey.HasItem)
					{
						var (item, passToIndex) = monkey.PerformInspection(adjustWorryLevel, worryReducingMod);
						monkeys[passToIndex].EnqueueItem(item);
					}
				}
				numberOfRounds--;
			}
			return this;
		}

		public long CalculateMonkeyBusiness(int numberOfMonkeys)
		{
			if (monkeys.Count < numberOfMonkeys)
				throw new InvalidOperationException($"Monkey business cannot be calculated using the top {numberOfMonkeys} monkeys as there are {monkeys.Count} monkeys in the group.");

			var topMonkeys = monkeys.Select(x => x.ItemsInspected).OrderByDescending(x => x).Take(numberOfMonkeys).ToArray();
			long monkeyBusiness = 1;
			foreach (var monkeyScore in topMonkeys) monkeyBusiness *= monkeyScore;
			return monkeyBusiness;
		}
	}
}
