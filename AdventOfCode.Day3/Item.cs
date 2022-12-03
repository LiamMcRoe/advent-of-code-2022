namespace AdventOfCode.Day3
{
	public record Item(char ItemCode)
	{
		public int Priority
		{
			get
			{
				var upperItem = char.ToUpper(ItemCode);
				return (ItemCode == upperItem) ? upperItem - 38 : upperItem - 64;
			}
		}
	}
}
