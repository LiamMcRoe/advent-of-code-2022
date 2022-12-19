using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Blocks
{
    public class CrossBlock : Block
    {
        public CrossBlock(int wallIndex, long highestBlock)
        {
            CurrentPoints.Add(new Point(wallIndex + 4, highestBlock + 4));
            CurrentPoints.Add(new Point(wallIndex + 3, highestBlock + 5));
            CurrentPoints.Add(new Point(wallIndex + 4, highestBlock + 5));
            CurrentPoints.Add(new Point(wallIndex + 5, highestBlock + 5));
            CurrentPoints.Add(new Point(wallIndex + 4, highestBlock + 6));
        }
    }
}
