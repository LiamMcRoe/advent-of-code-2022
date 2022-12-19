﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Blocks
{
    public class HorizontalLineBlock : Block
    {
        public HorizontalLineBlock(int wallIndex, long highestBlock)
        {
            CurrentPoints.Add(new Point(wallIndex + 3, highestBlock + 4));
            CurrentPoints.Add(new Point(wallIndex + 4, highestBlock + 4));
            CurrentPoints.Add(new Point(wallIndex + 5, highestBlock + 4));
            CurrentPoints.Add(new Point(wallIndex + 6, highestBlock + 4));
        }
    }
}
