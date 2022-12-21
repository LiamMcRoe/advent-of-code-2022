using AdventOfCode.Day17.Blocks;

namespace AdventOfCode.Day17
{
	public class PlayingSurface
	{
		private readonly string moves;
		private readonly int leftWallIndex;
		private readonly int rightWallIndex;
		private readonly HashSet<Point> blockedPoints;
		private readonly List<KnownState> knownStates;

		private long highestPoint;

		private bool moveDown;
		private int moveIndex;

		private bool cycleFound;
		private long cycleStartAfter;
		private long cycleEndAfter;
		private long heightAddedByCycle;

		public PlayingSurface(string moves)
		{
			this.moves = moves;
			leftWallIndex = 0;
			rightWallIndex = 8;
			blockedPoints = new HashSet<Point>();
			for (int i = leftWallIndex; i <= rightWallIndex; i++)
			{
				blockedPoints.Add(new Point(i, 0));
			}
			knownStates = new List<KnownState>();
			moveDown = false;
			moveIndex = 0;
			cycleFound = false;
		}

		public long SimulateBlocks(long blocksToDrop)
		{
			for (long i = 1; i <= blocksToDrop; i++)
			{
				DropBlock(i);
				if (cycleFound)
				{
					return SimulateFromCycleFound(blocksToDrop, i);
				}
			}
			return highestPoint;
		}

		private void DropBlock(long blockNumber)
		{
			var block = BlockFactory.GetBlock(blockNumber, leftWallIndex, highestPoint);
			var stillMoving = true;
			while (stillMoving)
			{
				stillMoving = MoveBlock(block);
			}

			foreach (var point in block.CurrentPoints)
			{
				highestPoint = point.Y > highestPoint ? point.Y : highestPoint;
				blockedPoints.Add(point);
			}
			var blockedLineY = GetHighestBlockedLine();
			var pointsInState = blockedPoints.Where(p => p.Y > blockedLineY);
			var shiftedPoints = pointsInState.Select(p => new Point(p.X, p.Y - blockedLineY)).ToHashSet();
			var state = new KnownState(shiftedPoints, moveIndex, blockNumber, highestPoint);
			var cycleStart = knownStates.Where(s => s.EquivalentToState(state)).FirstOrDefault();
			if (!cycleFound && cycleStart != null)
			{
				cycleStartAfter = cycleStart.BlocksDropped;
				cycleEndAfter = blockNumber;
				heightAddedByCycle = state.Height - cycleStart.Height;
				cycleFound = true;
				return;
			}
			knownStates.Add(state);
		}

		private bool MoveBlock(Block block)
		{
			if (moveDown)
			{
				var isBlocked = block.MoveDown(blockedPoints);
				moveDown = false;
				return isBlocked;
			}
			else
			{
				var move = moves[moveIndex];
				if (move == '>') block.MoveRight(blockedPoints, rightWallIndex);
				else block.MoveLeft(blockedPoints, leftWallIndex);
				moveIndex = (moveIndex + 1 ) % moves.Length;
				moveDown = true;
				return true;
			}
		}

		private long SimulateFromCycleFound(long blocksToDrop, long blocksDropped)
		{
			var cycleLength = cycleEndAfter - cycleStartAfter;
			var leftToDrop = blocksToDrop - blocksDropped;
			var numCyclesLeft = leftToDrop / cycleLength;
			var iterationsAfterCycles = leftToDrop - (numCyclesLeft * cycleLength);

			for (long j = blocksDropped + 1; j <= blocksDropped + iterationsAfterCycles; j++)
			{
				DropBlock(j);
			}
			return highestPoint + (numCyclesLeft * heightAddedByCycle);
		}

		private long GetHighestBlockedLine() => blockedPoints.Select(p => p.Y).Where(y => LineBlocked(y)).Max();

		private bool LineBlocked(long y)
		{
			for (int i = leftWallIndex + 1; i < rightWallIndex; i++)
			{
				if (!blockedPoints.Contains(new Point(i, y))) return false;
			}
			return true;
		}
	}
}
