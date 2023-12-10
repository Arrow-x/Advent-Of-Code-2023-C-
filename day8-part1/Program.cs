string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false)
	return;
var lines = File.ReadLines(data).ToArray<string>();
var instructions = lines[0]
					   .Select(x => x switch {
						   'L' => 0,
						   'R' => 1,
						   _ => -1,
					   })
					   .ToArray<int>();
var map = new Dictionary<string, string[]>();
var splitOptions =
	StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
string[] split;
string[] leftSide;
for (int line = 2; line < lines.Length; line++) {
	split = lines[line].Split("=", splitOptions);
	leftSide = split[1].Split(",", splitOptions);
	leftSide[0] = leftSide[0].Remove(0, 1);
	leftSide[1] = leftSide[1].Remove(leftSide[1].Length - 1, 1);
	map[split[0]] = leftSide;
}
string nextStep = map["AAA"][instructions[0]];
int steps = 1;
int start = 1;
while (steps != -1) {
	for (int i = start; i < instructions.Length; i++) {
		// Console.WriteLine("{0} i: {1}", nextStep, instructions[i]);
		nextStep = map[nextStep][instructions[i]];
		steps++;
		if (nextStep == "ZZZ") {
			Console.WriteLine(steps);
			steps = -1;
			break;
		}
	}
	start = 0;
}
