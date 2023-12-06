string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;
var lines = File.ReadLines(data).ToArray<string>();

var splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
var raceTime = long.Parse(lines[0].Split(":", splitOptions)[1].Replace(" ", string.Empty));
var recordDistance = long.Parse(lines[1].Split(":", splitOptions)[1].Replace(" ", string.Empty));

var min = ((long)(raceTime + Math.Sqrt(Math.Pow(raceTime, 2) - (4 * recordDistance)) / -2));
var plus = ((long)(raceTime - Math.Sqrt(Math.Pow(raceTime, 2) - (4 * recordDistance)) / -2));
Console.WriteLine($"{plus - min}");
