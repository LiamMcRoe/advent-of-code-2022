namespace AdventOfCode.Day3
{
	public class Backpack
	{
		private readonly int compartmentSize;
		private List<Item> CompartmentOne => Contents.GetRange(0, compartmentSize);
		private List<Item> CompartmentTwo => Contents.GetRange(compartmentSize, compartmentSize);

		public Backpack(string contents)
		{
			this.Contents = contents.Select(x => new Item(x)).ToList();
			this.compartmentSize = this.Contents.Count / 2;
		}

		public List<Item> Contents { get; }

		public int GetRepeatedItemPriority() => CompartmentOne.First(x => CompartmentTwo.Contains(x)).Priority;
	}
}
