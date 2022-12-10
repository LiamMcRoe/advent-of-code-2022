using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
	public static class Day10
	{
		public static void Run(string inputPath)
		{
			var instructions = File.ReadAllLines(inputPath);
			var processor = new Processor(new Screen(6, 40, '█', '░'), 20, 40);
			processor.ProcessInstructions(instructions);

			Console.WriteLine($"Total measured signal strength (part one): {processor.TotalSignalStrength}");
			Console.WriteLine($"Image drawn by processor instructions (part two):");
			processor.Screen.ConsoleRender();
		}
	}
}
