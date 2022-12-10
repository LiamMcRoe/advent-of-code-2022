namespace AdventOfCode.Day10
{
	public class Processor
	{
		private int cycleNumber;
		private int register;
		private readonly int captureSignalStart;
		private readonly int captureSignalFrequency;

		public Processor(Screen screen, int captureSignalStart, int captureSignalFrequency)
		{
			Screen = screen;
			this.captureSignalStart = captureSignalStart;
			this.captureSignalFrequency = captureSignalFrequency;
			cycleNumber = 1;
			register = 1;
		}

		public int TotalSignalStrength { get; private set; }

		public Screen Screen { get; init; }

		public void ProcessInstructions(IEnumerable<string> instructions)
		{
			foreach (var instruction in instructions) ProcessSingleInstruction(instruction);
		}

		private void ProcessSingleInstruction(string instruction)
		{
			switch (instruction)
			{
				case "noop":
					PerformCycles(1);
					break;
				default:
					PerformCycles(2);
					var toAdd = int.Parse(instruction.Split(' ')[1]);
					register += toAdd;
					break;
			}
		}

		private void PerformCycles(int numberOfCycles)
		{
			while (numberOfCycles != 0)
			{
				var column = (cycleNumber - 1) % Screen.Width;
				var row = (cycleNumber - 1 - column) / Screen.Width;
				if (register - 1 <= column && column <= register + 1) Screen.DrawPixel(row, column);
				if (cycleNumber % captureSignalFrequency == captureSignalStart) TotalSignalStrength += (cycleNumber * register);
				cycleNumber++;
				numberOfCycles--;
			}
		}
	}
}
