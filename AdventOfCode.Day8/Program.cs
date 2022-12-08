// See https://aka.ms/new-console-template for more information

var input = File.ReadAllLines("Input.txt");

var part1 = input
    .Select((x, i) =>
        x.Where((y, j) =>
            i == 0 ||
            i == input.Length - 1 ||
            j == 0 ||
            j == input[i].Length - 1 ||
            input[..i].Select(x => x[j]).All(k => !(k >= y)) ||
            input[(i + 1)..].Select(x => x[j]).All(k => !(k >= y)) ||
            input[i][..j].All(k => !(k >= y)) ||
            input[i][(j + 1)..].All(k => !(k >= y)))
        .Count())
    .Sum();

Console.WriteLine($"Part one: {part1}");