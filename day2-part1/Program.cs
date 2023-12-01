using System.Text.RegularExpressions;

string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

string? line;
int total = 0;

bool TestDraw(string[] sets)
{
    foreach (var g in sets)
    {
        foreach (var n in g.Split(","))
        {
            var match = Regex.Match(n.Trim(), @"(\d+) (.*)");
            var rockCount = int.Parse(match.Groups[1].Value);
            var rockColor = match.Groups[2].Value;
            bool isOK = rockColor switch
            {
                "blue" => rockCount <= 14,
                "green" => rockCount <= 13,
                "red" => rockCount <= 12,
                _ => true
            };
            if (isOK == false)
            {
                return false;
            }
        }
    }
    return true;
}
using (var fs = File.OpenText(data))
{
    while ((line = fs.ReadLine()) != null)
    {
        Match matchLine = Regex.Match(line, @"Game (\d+): (.*)");
        int currentGameID = int.Parse(matchLine.Groups[1].Value);
        if (TestDraw(matchLine.Groups[2].Value.Split(";")) == true)
        {
            total += currentGameID;
        }
    }
}
Console.WriteLine(total);
