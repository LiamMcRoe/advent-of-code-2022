﻿
Func<int, int> getFirstDistinctChunk = chunkSize => new[] { File.ReadAllText("Input.txt") }
	.Select(s =>
		Enumerable.Range(0, s.Length - (chunkSize + 1))
		.Select(i => (i + chunkSize, s.Substring(i, chunkSize)))
		.First(x => x.Item2.Distinct().Count() == chunkSize)
		.Item1)
	.First();

Console.WriteLine($"Part one: {getFirstDistinctChunk(4)}");
Console.WriteLine($"Part two: {getFirstDistinctChunk(14)}");
