using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayOne
{
    public static class DayOne
    {
        public static void Run(string inputPath)
        {
            var caloriesByElf = GetSortedCalories(inputPath);
            Console.WriteLine($"Most cals held by an elf: {caloriesByElf[0]}");
            Console.WriteLine($"Total cals held by top 3 elves: {caloriesByElf.Take(3).Sum()}");
        }

        private static List<int> GetSortedCalories(string inputPath)
        {
            List<int> caloriesByElf = new List<int>();
            using var sr = File.OpenText(inputPath);
            string? line;
            int currentElfTotal = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    caloriesByElf.Add(currentElfTotal);
                    currentElfTotal = 0;
                }
                else
                {
                    currentElfTotal += int.Parse(line);
                }
            }
            caloriesByElf.Add(currentElfTotal); // Ensure final elf is counted.
            caloriesByElf.Sort((a, b) => b.CompareTo(a));
            return caloriesByElf;
        }
    }
}
