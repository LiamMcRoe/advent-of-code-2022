using System.Diagnostics;

namespace AdventOfCode.Day20
{
	public static class Day20
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllLines(inputPath);
			PartOne(input);
			PartTwo(input);
		}

		private static void PartOne(string[] input)
		{
			var fileToMix = input.Select((x, i) => new FileElement(i, long.Parse(x))).ToList();
			Mix(fileToMix, 1);
			var coord = GetCoordinates(fileToMix);
			Console.WriteLine($"Co-ords without key (part one): {coord}");
		}

		private static void PartTwo(string[] input)
		{
			var fileToMix = input.Select((x, i) => new FileElement(i, 811589153 * long.Parse(x))).ToList();
			Mix(fileToMix, 10);
			var coord = GetCoordinates(fileToMix);
			Console.WriteLine($"Co-ords with key (part two): {coord}");
		}

		private static void Mix(List<FileElement> fileToMix, int timesToMix)
		{
			var timesMixed = 0;
			while (timesMixed < timesToMix)
			{
				for (int i = 0; i < fileToMix.Count; i++)
				{
					var currentElement = fileToMix.First(x => x.OriginalPosition == i);
					var currentIndex = fileToMix.IndexOf(currentElement);

					if (currentElement.Value == 0) continue;
					fileToMix.MoveForward(currentIndex, currentElement.Value % (fileToMix.Count - 1));
				}
				timesMixed++;
			}
		}

		private static long GetCoordinates(List<FileElement> file)
		{
			var zeroIndex = file.IndexOf(file.First(x => x.Value == 0));
			return file[(zeroIndex + 1000) % file.Count].Value + file[(zeroIndex + 2000) % file.Count].Value + file[(zeroIndex + 3000) % file.Count].Value;
		}
	}
}
