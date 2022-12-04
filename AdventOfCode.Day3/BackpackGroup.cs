namespace AdventOfCode.Day3
{
	public class BackpackGroup
	{
		private List<Backpack> backpacks;

		public BackpackGroup(string[] backpackDefinitions)
		{
			this.backpacks = backpackDefinitions.Select(x => new Backpack(x)).ToList();
		}

		public int GetTotalRepeatedItemsPriority() => backpacks.Select(x => x.GetRepeatedItemPriority()).Sum();

		public int GetTotalBadgePriority()
		{
			var score = 0;
			for (int i = 0; i < backpacks.Count; i += 3)
			{
				score += backpacks[i].AllContents.First(x => backpacks[i + 1].AllContents.Contains(x) && backpacks[i + 2].AllContents.Contains(x)).Priority;
			}
			return score;
		}
	}
}
