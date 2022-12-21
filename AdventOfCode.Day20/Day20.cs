namespace AdventOfCode.Day20
{
	public static class Day20
	{
		public static void Run(string inputPath)
		{
			var input = File.ReadAllLines(inputPath).Select((x, i) => new FileElement(i, 811589153 * long.Parse(x))).ToList();
			var toProcess = File.ReadAllLines(inputPath).Select((x,i) => new FileElement(i, 811589153 * long.Parse(x))).ToList();
			var numberElements = input.Count;
			var processedElements = new HashSet<FileElement>();
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine($"i = {i}");
				foreach (var element in toProcess)
				{
					var currentIndex = input.IndexOf(element);
					var item = input[currentIndex];
					if (item.Value == 0) continue;
					input.MoveForward(currentIndex, item.Value);
				}
			}
			var zeroIndex = input.IndexOf(input.First(x => x.Value == 0));
			Console.WriteLine(input[(zeroIndex + 1000) % numberElements].Value + input[(zeroIndex + 2000) % numberElements].Value + input[(zeroIndex + 3000) % numberElements].Value);
		}
	}
}
