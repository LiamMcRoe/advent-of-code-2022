using System.Text.RegularExpressions;

namespace AdventOfCode.Day22
{
	public class Map
	{
		private readonly string[] gridDefinition;
		private readonly int lineLength;
		private Facing currentFacing;
		private (int Row, int Col) currentPosition;

		public Map(string[] gridDefinition)
		{
			lineLength = gridDefinition.Select(x => x.Length).Max();
			for (int i = 0; i < gridDefinition.Length; i ++)
			{
				gridDefinition[i] = PadString(gridDefinition[i], lineLength);
			}
			this.gridDefinition = gridDefinition;
			currentFacing = Facing.Right;
			var startCol = gridDefinition[0].IndexOf('.');
			currentPosition = (0, startCol);
		}

		public int CalculatePassword(string path)
		{
			var numbers = new Queue<int>(path.Split(new char[] { 'L', 'R' }).Select(x => int.Parse(x)));
			var directions = new Queue<char>(Regex.Replace(path, @"[\d]", string.Empty));
			var numberNext = char.IsNumber(path[0]);
			while (numbers.Any() || directions.Any())
			{
				if (numberNext) 
				{
					var moveBy = numbers.Dequeue();
					Move(moveBy);
					numberNext = false;
				}
				else
				{
					var direction = directions.Dequeue();
					ChangeDirection(direction);
					numberNext = true;
				}
			}

			return ((currentPosition.Row + 1) * 1000) + ((currentPosition.Col + 1) * 4) + (int)currentFacing;
		}

		private void Move(int moveBy)
		{
			while (moveBy > 0)
			{
				var nextPosition = GetNextPosition();
				if (nextPosition.Col < 0 || nextPosition.Col > lineLength - 1 || nextPosition.Row < 0 || nextPosition.Row > gridDefinition.Length - 1 || ReadGrid(nextPosition) == ' ')
				{
					nextPosition = GetWrappedPosition();
				}
				var nextChar = ReadGrid(nextPosition);
				if (nextChar == '.')
				{
					currentPosition = nextPosition;
					moveBy--;
				}
				if (nextChar == '#')
				{
					moveBy = 0;
				}
			}
		}

		private (int Row, int Col) GetNextPosition() => currentFacing switch
		{
			Facing.Up => (currentPosition.Row - 1, currentPosition.Col),
			Facing.Down => (currentPosition.Row + 1, currentPosition.Col),
			Facing.Left => (currentPosition.Row, currentPosition.Col - 1),
			Facing.Right => (currentPosition.Row, currentPosition.Col + 1),
			_ => throw new InvalidOperationException()
		};

		private (int Row, int Col) GetWrappedPosition()
		{
			var row = currentPosition.Row;
			var column = currentPosition.Col;
			switch (currentFacing)
			{
				case Facing.Right:
					column = gridDefinition[currentPosition.Row].TakeWhile(x => x == ' ').Count();
					break;
				case Facing.Left:
					column = gridDefinition[currentPosition.Row].Length - gridDefinition[currentPosition.Row].Reverse().TakeWhile(x => x == ' ').Count() - 1;
					break;
				case Facing.Up:
					var currCol = gridDefinition.Select(s => s[currentPosition.Col]);
					row = currCol.Count() - currCol.Reverse().TakeWhile(x => x == ' ').Count() - 1;
					break;
				case Facing.Down:
					row = gridDefinition.Select(s => s[currentPosition.Col]).TakeWhile(x => x == ' ').Count();
					break;
			}
			return (row, column);
		}

		private void ChangeDirection(char direction)
		{
			var incrementor = direction == 'R' ? 1 : -1;
			var newFacing = ((int)currentFacing + incrementor) % 4;
			currentFacing = newFacing >=0 ? (Facing)newFacing : Facing.Up;
		}

		private char ReadGrid((int Row, int Col) position) => gridDefinition[position.Row][position.Col];

		private static string PadString(string toPad, int padToLength)
		{
			while (toPad.Length < padToLength) toPad += " ";
			return toPad;
		}
	}
}
