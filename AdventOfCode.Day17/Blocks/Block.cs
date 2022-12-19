using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Blocks
{
    public abstract class Block
    {
        public Block()
        {
            CurrentPoints = new List<Point>();
        }

        public List<Point> CurrentPoints { get; set; }

        public bool MoveDown(HashSet<Point> blockedPoints)
        {
            var newPoints = CurrentPoints.Select(p => new Point(p.X, p.Y - 1)).ToList();
            if (newPoints.Any(p => blockedPoints.Contains(p)))
            {
                return false;
            }
            CurrentPoints = newPoints;
            return true;
        }

        public void MoveLeft(HashSet<Point> blockedPoints, int leftWallIndex)
        {
            var newPoints = CurrentPoints.Select(p => new Point(p.X - 1, p.Y)).ToList();
			if (newPoints.Any(p => blockedPoints.Contains(p) || p.X <= leftWallIndex))
			{
                return;
			}
			CurrentPoints = newPoints;
		}

        public void MoveRight(HashSet<Point> blockedPoints, int rightWallIndex)
        {
            var newPoints = CurrentPoints.Select(p => new Point(p.X + 1, p.Y)).ToList();
			if (newPoints.Any(p => blockedPoints.Contains(p) || p.X >= rightWallIndex))
			{
				return;
			}
			CurrentPoints = newPoints;
		}
    }
}
