namespace AdventOfCode.Day3
{
	public class Backpack
	{
		private readonly int compartmentSize;

		public Backpack(string contents)
		{
			this.AllContents = contents.Select(x => new Item(x)).ToList();
			this.compartmentSize = this.AllContents.Count / 2;
		}

		public List<Item> AllContents { get; }

		public List<Item> CompartmentOneContents => AllContents.GetRange(0, compartmentSize);

		public List<Item> CompartmentTwoContents => AllContents.GetRange(compartmentSize, compartmentSize);

		public int GetRepeatedItemPriority() => CompartmentOneContents.First(x => CompartmentTwoContents.Contains(x)).Priority;
	}
}
