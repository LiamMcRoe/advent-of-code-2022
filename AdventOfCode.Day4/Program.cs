
// Went for some quick and dirty LINQ one-liners today.
var part1 = File.ReadAllLines("Input.txt")
	.Select(x => x.Split(',').Select(y => y.Split('-').Select(z => int.Parse(z)).ToArray()).ToArray())
	.Where(x =>
		(x[0][0] <= x[1][0] && x[0][1] >= x[1][1]) ||
		(x[1][0] <= x[0][0] && x[1][1] >= x[0][1]))
	.Count();

var part2 = File.ReadAllLines("Input.txt")
	.Select(x => x.Split(',').Select(y => y.Split('-').Select(z => int.Parse(z)).ToArray()).ToArray())
	.Where(x =>
		(x[0][0] <= x[1][1] && x[1][0] <= x[0][1]))
	.Count();

Console.WriteLine($"Part one (ranges fully contained in partners'): {part1}");
Console.WriteLine($"Part two (ranges ranges where there is overlap with partners'): {part2}");