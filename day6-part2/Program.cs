string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

var lines = File.ReadLines(data).ToArray<string>();
var splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
var raceTimes = ulong.Parse(lines[0].Split(":", splitOptions)[1].Replace(" ", string.Empty));
var recordDistances = ulong.Parse(lines[1].Split(":", splitOptions)[1].Replace(" ", string.Empty));
ulong raceRecord = 0;
for (ulong time = 0; time <= raceTimes; time++)
{
    if (time * (raceTimes - time) > recordDistances)
    {
        raceRecord += 1;
    }
}
Console.WriteLine(raceRecord);
