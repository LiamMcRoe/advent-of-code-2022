
// I would like to formally apologise to any and all developers who worked on LINQ for this egregious abuse of their hard work.

var input = File.ReadAllLines("Input.txt");
var part1 = input
    .Select((x, i) =>
        x.Where((y, j) =>
            i == 0 ||
            i == input.Length - 1 ||
            j == 0 ||
            j == input[i].Length - 1 ||
            input[..i].Select(x => x[j]).All(k => (k < y)) ||
            input[(i + 1)..].Select(x => x[j]).All(k => (k < y)) ||
            input[i][..j].All(k => (k < y)) ||
            input[i][(j + 1)..].All(k => (k < y)))
        .Count())
    .Sum();

var part2 = input
	.Select((x, i) =>
		x.Select((y, j) =>
			i == 0 || i == input.Length - 1 || j == 0 || j == input[i].Length - 1 ? 0 :
			    new[] { input[..i].Select(x => x[j]).Reverse().TakeWhile(x => x < y).Count() }.Select(c => c < i ? c + 1 : c).Single() *
                new[] { input[(i + 1)..].Select(x => x[j]).TakeWhile(x => x < y).Count() }.Select(c => c < input.Length - i - 1 ? c + 1 : c).Single() *
                new[] { input[i][..j].Reverse().TakeWhile(x => x < y).Count() }.Select(c => c < j ? c + 1 : c).Single() *
                new[] { input[i][(j + 1)..].TakeWhile(x => x < y).Count() }.Select(c => c < input[i].Length - j - 1 ? c + 1 : c).Single())
        .Max())
	.Max();

Console.WriteLine($"Part one: {part1}");
Console.WriteLine($"Part two: {part2}");