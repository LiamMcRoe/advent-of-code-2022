namespace AdventOfCode.Day7
{
	public class FileSystem
	{
		public FileSystem(string[] mapInstructions)
		{
			Root = new Node();
			MapFileSystem(mapInstructions);
		}

		public Node Root { get; set; }

		public int GetRequiredFreeSpace(int totalDiskSpace, int totalRequiredSpace) => totalRequiredSpace - totalDiskSpace + Root.GetSize();

		private void MapFileSystem(string[] mapInstructions)
		{
			var currentDirectory = Root;
			foreach (var instruction in mapInstructions)
			{
				currentDirectory = ProcessInstruction(currentDirectory, instruction);
			}
		}

		private Node ProcessInstruction(Node currentDirectory, string instruction)
		{
			var decomposedInstruction = instruction.Split(' ');
			return decomposedInstruction[0] switch
			{
				"$" => decomposedInstruction[1] == "cd" ? ChangeDirectory(currentDirectory, decomposedInstruction[2]) : currentDirectory,
				"dir" => currentDirectory.AddChild(decomposedInstruction[1], new Node(currentDirectory)),
				_ => currentDirectory.IncreaseSize(int.Parse(decomposedInstruction[0])),
			};
		}

		private Node ChangeDirectory(Node currentDirectory, string newDirectory) => newDirectory switch
		{
			".." => currentDirectory.Parent ?? throw new InvalidOperationException("Current node has no parent."),
			"/" => Root,
			_ => currentDirectory.AddChild(newDirectory, new Node(currentDirectory)).GetChild(newDirectory),
		};
	}
}
