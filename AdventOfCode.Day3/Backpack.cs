namespace AdventOfCode.Day3
{
	public class Backpack
	{
		private readonly List<Item> compartmentOne;
		private readonly List<Item> compartmentTwo;

		public Backpack(string contents)
		{
			var allItems = contents.Select(x => new Item(x)).ToList();
			var compartmentSize = allItems.Count / 2;
			this.compartmentOne = allItems.GetRange(0, compartmentSize);
			this.compartmentTwo = allItems.GetRange(compartmentSize, compartmentSize);
		}

		public IEnumerable<Item> Contents => compartmentOne.Concat(compartmentTwo);

		public int GetRepeatedItemPriority() => compartmentOne.First(x => compartmentTwo.Contains(x)).Priority;
	}
}
