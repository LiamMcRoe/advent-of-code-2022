using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day20
{
	public static class ListExtensions
	{
		public static void MoveForward<T>(this List<T> list, int index, long moveBy)
		{
			if (moveBy == 0) return;
			if (moveBy > 0)
			{
				while (moveBy > 0)
				{
					var toMove = list[index];
					var adjacent = list[(index + 1) % list.Count];
					list[index] = adjacent;
					list[(index + 1) % list.Count] = toMove;
					index = (index + 1) % list.Count;
					moveBy--;
				}
			}
			else
			{
				while (moveBy < 0)
				{
					var toMove = list[index];

					var newIndex = (index - 1);
					newIndex = newIndex < 0 ? list.Count - 1 : newIndex; // Shouldn't be swapping here, everything should move down
					var adjacent = list[newIndex];
					list[index] = adjacent;
					
					list[newIndex] = toMove;
					index = newIndex;
					moveBy++;
				}
			}
		}
	}
}
