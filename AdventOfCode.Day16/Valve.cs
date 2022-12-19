using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day16
{
	internal class Valve
	{
		private const string codePattern = "(?<=Valve ).*?(?= has)";
		private const string flowRatePattern = @"\d+";
		private const string connectedToPattern = @"(?<=to valve).*";

		public readonly Dictionary<string, int> knownStates;
		private readonly List<Valve> connectedValves;

		public Valve(string valve)
		{
			knownStates = new Dictionary<string, int>();
			connectedValves = new List<Valve>();
			ValveCode = Regex.Match(valve, codePattern).Value;
			FlowRate = int.Parse(Regex.Match(valve, flowRatePattern).Value);
			ConnectedCodes = new List<string>();
			var match = Regex.Match(valve, connectedToPattern).Value.Replace("s ", "").Trim();
			ConnectedCodes.AddRange(match.Split(", "));
		}

		public string ValveCode { get; init; }

		public int FlowRate { get; init; }

		public List<string> ConnectedCodes { get; init; }

		public void AddConnectedValve(Valve valve) => connectedValves.Add(valve);

		public int CalculateMaxPressueReleased(int minsLeft, int currentFlowRate, HashSet<string> openValves)
		{
			if (minsLeft == 0) return 0;
			if (minsLeft == 1) return currentFlowRate;

			// Check if we have hit this exact state before, ie same valves open with same time left. If so, just return the known answer.
			var stateKey = GetStateKey(minsLeft, openValves);
			if (knownStates.TryGetValue(stateKey, out var score)) return score;

			var maxPressureReleased = 0;
			foreach (var childValve in connectedValves)
			{
				var childMaxPressureReleased = CalculateChildMaxPressureReleased(childValve, minsLeft, currentFlowRate, openValves);
				maxPressureReleased = childMaxPressureReleased > maxPressureReleased ? childMaxPressureReleased : maxPressureReleased;
			}

			if (maxPressureReleased != 0) knownStates.Add(stateKey, maxPressureReleased);
			return maxPressureReleased;
		}

		private int CalculateChildMaxPressureReleased(Valve childValve, int minsLeft, int currentFlowRate, HashSet<string> openValves)
		{
			var openIncludingThis = new HashSet<string>(openValves);
			var scoreWithoutOpening = childValve.CalculateMaxPressueReleased(minsLeft - 1, currentFlowRate, openIncludingThis) + currentFlowRate;
			var scoreWithOpening = 0;
			if (FlowRate > 0 && !openIncludingThis.Contains(ValveCode))
			{
				openIncludingThis.Add(ValveCode);
				scoreWithOpening = childValve.CalculateMaxPressueReleased(minsLeft - 2, currentFlowRate + FlowRate, openIncludingThis) + (2 * currentFlowRate) + FlowRate;
			}
			
			return (scoreWithOpening > scoreWithoutOpening) ? scoreWithOpening : scoreWithoutOpening;	
		}

		private static string GetStateKey(int minsLeft, HashSet<string> openValves) => string.Join(',', openValves.OrderBy(x => x)) + $"[{minsLeft}]";
	}
}
