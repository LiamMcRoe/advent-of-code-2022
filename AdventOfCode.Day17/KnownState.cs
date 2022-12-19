namespace AdventOfCode.Day17
{
	public class KnownState
	{
		public KnownState(HashSet<Point> blockedPointsAboveFloor, int moveIndex, long blocksDropped, long height)
		{
			this.BlockedPointsAboveFloor = blockedPointsAboveFloor;
			this.MoveIndex = moveIndex;
			this.BlocksDropped = blocksDropped;
			this.Height = height;
		}

		private HashSet<Point> BlockedPointsAboveFloor { get; init; }
		private int MoveIndex { get; init; }
		public long BlocksDropped { get; init; }

		public long Height { get; init; }

		public bool EquivalentToState(KnownState newState) =>
			BlockedPointsAboveFloor.SequenceEqual(newState.BlockedPointsAboveFloor) && MoveIndex == newState.MoveIndex && BlocksDropped % 5 == newState.BlocksDropped % 5;
	}
}
