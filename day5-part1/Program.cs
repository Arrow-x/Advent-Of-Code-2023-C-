string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

var lines = File.ReadLines(data).ToArray<string>();

var seeds = new uint[4];

var splits = new List<uint[]>();
var seedToSoil = new List<uint[]>();
var soilToFertilizer = new List<uint[]>();
var fertilizerToWater = new List<uint[]>();
var waterToLight = new List<uint[]>();
var lightToTemperature = new List<uint[]>();
var temperatureToHumidity = new List<uint[]>();
var humidityToLocation = new List<uint[]>();


foreach (var line in lines)
{
    if (line == "")
    {
        continue;
    }
    else if (line.Contains("seeds: ") == true)
    {
        seeds = PrseLine(line.Remove(0, 7));
        continue;
    }
    else if (line.Contains("seed-to-soil map:") == true)
    {
        splits = seedToSoil;
        continue;
    }
    else if (line.Contains("soil-to-fertilizer map:") == true)
    {
        splits = soilToFertilizer;
        continue;
    }
    else if (line.Contains("fertilizer-to-water map:") == true)
    {
        splits = fertilizerToWater;
        continue;
    }
    else if (line.Contains("water-to-light map:") == true)
    {
        splits = waterToLight;
        continue;
    }
    else if (line.Contains("light-to-temperature map:") == true)
    {
        splits = lightToTemperature;
        continue;
    }
    else if (line.Contains("temperature-to-humidity map:") == true)
    {
        splits = temperatureToHumidity;
        continue;
    }
    else if (line.Contains("humidity-to-location map:") == true)
    {
        splits = humidityToLocation;
        continue;
    }
    splits.Add(PrseLine(line.Trim()));
}

uint resault = 0;
foreach (var seed in seeds)
{
    var soil = CheckRange(seed, seedToSoil);
    var fert = CheckRange(soil, soilToFertilizer);
    var wat = CheckRange(fert, fertilizerToWater);
    var light = CheckRange(wat, waterToLight);
    var temp = CheckRange(light, lightToTemperature);
    var hum = CheckRange(temp, temperatureToHumidity);
    var loc = CheckRange(hum, humidityToLocation);
    if (resault == 0 || loc < resault)
    {
        resault = loc;
    }
}
uint CheckRange(uint seed, List<uint[]> map)
{
    uint indexed = seed;
    foreach (var idx in map)
    {
        if (seed >= idx[1] && seed < idx[1] + idx[2])
        {
            indexed = seed - idx[1] + idx[0];
            break;
        }
    }
    return indexed;
}

uint[] PrseLine(string inputLine)
{
    var splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
    return inputLine.Split(" ", splitOptions).Select(x => uint.Parse(x.ToString())).ToArray<uint>();
}

Console.WriteLine(resault);
