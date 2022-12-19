using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Blocks
{
    public class VerticalLineBlock : Block
    {
        public VerticalLineBlock(int wallIndex, long highestBlock)
        {
            CurrentPoints.Add(new Point(wallIndex + 3, highestBlock + 4));
            CurrentPoints.Add(new Point(wallIndex + 3, highestBlock + 5));
            CurrentPoints.Add(new Point(wallIndex + 3, highestBlock + 6));
            CurrentPoints.Add(new Point(wallIndex + 3, highestBlock + 7));
        }
    }
}
