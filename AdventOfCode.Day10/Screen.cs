using System.Text;

namespace AdventOfCode.Day10
{
	public class Screen
	{
		private readonly char drawingChar;
		private readonly char[,] drawingGrid;

		public Screen(int height, int width, char drawingChar, char whitespaceChar)
		{
			Height = height;
			Width = width;
			this.drawingChar = drawingChar;
			drawingGrid = new char[height, width];
			for (int i = 0; i < Height; i++)
				for (int j = 0; j < Width; j++)
					drawingGrid[i, j] = whitespaceChar;
		}

		public int Height { get; init; }
		public int Width { get; init; }

		public void DrawPixel(int row, int col)
		{
			drawingGrid[row, col] = drawingChar;
		}

		public void ConsoleRender()
		{
			for (int i = 0; i < Height; i++)
			{
				var line = new StringBuilder();
				for (int j = 0; j < Width; j++)
				{
					line.Append(drawingGrid[i, j]);
				}
				Console.WriteLine(line);
			}
		}
	}
}
