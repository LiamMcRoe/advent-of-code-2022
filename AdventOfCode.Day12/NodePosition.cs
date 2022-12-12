namespace AdventOfCode.Day12
{
	public record struct NodePosition(int IPosition, int JPosition)
	{
		public bool IsInBounds(int maxRowIndex, int maxColIndex) =>
			0 <= IPosition && IPosition <= maxRowIndex &&
			0 <= JPosition && JPosition <= maxColIndex;
	}
}
