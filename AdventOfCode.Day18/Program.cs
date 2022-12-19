
var cubes = File.ReadAllLines("Input.txt").Select(x => x.Split(',')).Select(x => (X:int.Parse(x[0]), Y:int.Parse(x[1]), Z:int.Parse(x[2])));
var unblockedSides = 0;
foreach (var cube in cubes)
{
	unblockedSides += GetUnconnetedSides(cube);
}


Console.WriteLine(unblockedSides);

int GetUnconnetedSides((int X, int Y, int Z) cube)
{
	var unconnected = 0;

	if (!cubes.Any(c => c == (cube.X + 1, cube.Y, cube.Z))) unconnected++;
	if (!cubes.Any(c => c == (cube.X - 1, cube.Y, cube.Z))) unconnected++;
	if (!cubes.Any(c => c == (cube.X, cube.Y + 1, cube.Z))) unconnected++;
	if (!cubes.Any(c => c == (cube.X, cube.Y - 1, cube.Z))) unconnected++;
	if (!cubes.Any(c => c == (cube.X, cube.Y, cube.Z + 1))) unconnected++;
	if (!cubes.Any(c => c == (cube.X, cube.Y, cube.Z - 1))) unconnected++;

	return unconnected;

}