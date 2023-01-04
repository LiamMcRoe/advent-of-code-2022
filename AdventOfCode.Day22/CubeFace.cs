using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day22
{
	public class CubeFace
	{
		public int Index { get; init; }

		public Range RowRange { get; init; }

		public Range ColRange { get; init; }

		private Dictionary<Facing, Func<(int Row, int Col), (int Row, int Col)>> moveFuncs;



	}
}
