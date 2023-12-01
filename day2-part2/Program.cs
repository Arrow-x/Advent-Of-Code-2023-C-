using System.Text.RegularExpressions;

string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

string? line;
int total = 0;

int TestDraw(string[] sets)
{
    int blue = 0;
    int green = 0;
    int red = 0;
    foreach (var g in sets)
    {
        foreach (var n in g.Split(","))
        {
            var pattern = @"(\d+) (.*)";
            var match = Regex.Match(n.Trim(), pattern);
            var rockCount = int.Parse(match.Groups[1].Value);
            var rockColor = match.Groups[2].Value;
            switch (rockColor)
            {
                case "blue":
                    if (rockCount > blue)
                    {
                        blue = rockCount;
                    }
                    break;
                case "green":
                    if (rockCount > green)
                    {
                        green = rockCount;
                    }
                    break;
                case "red":
                    if (rockCount > red)
                    {
                        red = rockCount;
                    }
                    break;
            }
        }
    }
    return blue * green * red;
}
using (var fs = File.OpenText(data))
{
    while ((line = fs.ReadLine()) != null)
    {
        string pattern = @"Game \d+: (.*)";
        Match matchLine = Regex.Match(line, pattern);
        total += TestDraw(matchLine.Groups[1].Value.Split(";"));
    }
}
Console.WriteLine(total);
