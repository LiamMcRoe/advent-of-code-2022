using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Blocks;

namespace AdventOfCode.Day17
{
    public static class BlockFactory
	{
		public static Block GetBlock(long blockNumber, int wallIndex, long highestBlock) => (blockNumber % 5) switch
		{
			1 => new HorizontalLineBlock(wallIndex, highestBlock),
			2 => new CrossBlock(wallIndex, highestBlock),
			3 => new LBlock(wallIndex, highestBlock),
			4 => new VerticalLineBlock(wallIndex, highestBlock),
			0 => new SquareBlock(wallIndex, highestBlock)
		};
		
	}
}
