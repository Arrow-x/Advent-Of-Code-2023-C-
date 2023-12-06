string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

var lines = File.ReadLines(data).ToArray<string>();
var splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
var raceTimes = long.Parse(lines[0].Split(":", splitOptions)[1].Replace(" ", string.Empty));
var recordDistances = long.Parse(lines[1].Split(":", splitOptions)[1].Replace(" ", string.Empty));
long raceRecord = 0;
for (long time = 0; time <= raceTimes; time++)
{
    if (time * (raceTimes - time) > recordDistances)
    {
        raceRecord += 1;
    }
}
Console.WriteLine(raceRecord);
