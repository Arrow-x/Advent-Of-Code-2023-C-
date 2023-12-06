string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

var lines = File.ReadLines(data).ToArray<string>();
var splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
var raceTimes = lines[0].Split(":", splitOptions)[1].Split(" ", splitOptions).Select(x => int.Parse(x.Trim())).ToArray<int>();
var recordDistances = lines[1].Split(":", splitOptions)[1].Split(" ", splitOptions).Select(x => int.Parse(x.Trim())).ToArray<int>();

int records = 1;
for (int race = 0; race < raceTimes.Length; race++)
{
    int raceRecord = 0;
    for (int time = 0; time <= raceTimes[race]; time++)
    {
        if (time * (raceTimes[race] - time) > recordDistances[race])
        {
            raceRecord += 1;
        }
    }
    records *= raceRecord;
}
Console.WriteLine(records);
