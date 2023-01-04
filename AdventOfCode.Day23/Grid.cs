using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Day23
{
    public class Grid
    {
        private readonly HashSet<Elf> elfPositions;
        private readonly Queue<Direction> directions;

        public Grid(string[] input)
        {
            elfPositions  = new HashSet<Elf>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#') elfPositions.Add(new Elf(Row: i, Column: j));   
                }
            }
            directions = new Queue<Direction>(new[] { Direction.North, Direction.South, Direction.West, Direction.East });
        }

        public int SimulateRounds(int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();
            var roundNumber = 1;
            while (roundNumber <= numberOfRounds)
            {
                var proposedMoves = GetProposedMoves();
                var duplicates = proposedMoves.GroupBy(x => x.MoveTo).Where(g => g.Count() > 1).Select(y => y.Key).ToHashSet();
                proposedMoves.RemoveAll(x => duplicates.Contains(x.MoveTo));

                if (!proposedMoves.Any())
                {
                    Console.WriteLine($"Round: {roundNumber}");
                    sw.Stop();
                    Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
                    break;
                }
                foreach (var move in proposedMoves)
                {
                    elfPositions.Remove(move.MoveFrom);
                    elfPositions.Add(move.MoveTo);
                }

                var direction = directions.Dequeue();
                directions.Enqueue(direction);
                roundNumber++;
            }
            return CalculateEmptyTiles();
        }

        private void Draw()
        {
            var minRow = elfPositions.MinBy(x => x.Row).Row;
            var maxRow = elfPositions.MaxBy(x => x.Row).Row;
            var minColumn = elfPositions.MinBy(x => x.Column).Column;
            var maxColumn = elfPositions.MaxBy(x => x.Column).Column;

            for (int i = minRow; i <= maxRow; i++)
            {
                var sb = new StringBuilder();
                for (int j = minColumn; j <= maxColumn; j++)
                {
                    if (elfPositions.Contains(new Elf(i, j)))
                    {
                        sb.Append('#');
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }
                Console.WriteLine(sb.ToString());
            }
        }

        private int CalculateEmptyTiles()
        {
            var minRow = elfPositions.MinBy(x => x.Row).Row;
            var maxRow = elfPositions.MaxBy(x => x.Row).Row;
            var minColumn = elfPositions.MinBy(x => x.Column).Column;
            var maxColumn = elfPositions.MaxBy(x => x.Column).Column;

            var area = (Math.Abs(maxRow - minRow) + 1 ) * (Math.Abs(maxColumn - minColumn) + 1);
            return area - elfPositions.Count;
        }

        private List<(Elf MoveFrom, Elf MoveTo)> GetProposedMoves()
        {
            var proposedMoves = new List<(Elf MoveFrom, Elf MoveTo)>();
            foreach (var elf in elfPositions)
            {
                var proposedMove = GetProposedMove(elf);
                if (proposedMove.HasValue) proposedMoves.Add(proposedMove.Value);
            }
            return proposedMoves;
        }

        private (Elf MoveFrom, Elf MoveTo)? GetProposedMove(Elf elf)
        {
            if (!HasAdjacentElves(elf)) return null;
                       
            foreach (var direction in directions)
            {
                var adjacent = Look(elf, direction);
                if (!elfPositions.Any(x => adjacent.Contains(x)))
                    return (elf, adjacent[1]);
            }
            return null;
        }

        private static Elf[] Look(Elf elf, Direction direction) => direction switch
        {
            Direction.North => new Elf[] { new Elf(elf.Row - 1, elf.Column - 1), new Elf(elf.Row - 1, elf.Column), new Elf(elf.Row - 1, elf.Column + 1) },
            Direction.South => new Elf[] { new Elf(elf.Row + 1, elf.Column - 1), new Elf(elf.Row + 1, elf.Column), new Elf(elf.Row + 1, elf.Column + 1) },
            Direction.East => new Elf[] { new Elf(elf.Row - 1, elf.Column + 1), new Elf(elf.Row, elf.Column + 1), new Elf(elf.Row + 1, elf.Column + 1) },
            Direction.West => new Elf[] { new Elf(elf.Row - 1, elf.Column - 1), new Elf(elf.Row, elf.Column - 1), new Elf(elf.Row + 1, elf.Column - 1) },
        };

        private bool HasAdjacentElves(Elf elf)
        {
            return elfPositions.Any(x => x == new Elf(elf.Row + 1, elf.Column) || x == new Elf(elf.Row - 1, elf.Column) || x == new Elf(elf.Row, elf.Column + 1) || x == new Elf(elf.Row, elf.Column - 1) || x == new Elf(elf.Row + 1, elf.Column + 1) || x == new Elf(elf.Row - 1, elf.Column - 1) || x == new Elf(elf.Row + 1, elf.Column - 1) || x == new Elf(elf.Row - 1, elf.Column + 1));
        }
    }
}
