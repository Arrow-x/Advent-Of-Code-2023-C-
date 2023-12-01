using System.Text.RegularExpressions;

string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

string? line;
int total = 0;
int Parser(string input)
{
    return input switch
    {
        "one" => 1,
        "two" => 2,
        "three" => 3,
        "four" => 4,
        "five" => 5,
        "six" => 6,
        "seven" => 7,
        "eight" => 8,
        "nine" => 9,
        _ => int.Parse(input)
    };
}
using (var fs = File.OpenText(data))
{
    var localResaults = new List<int>();
    while ((line = fs.ReadLine()) != null)
    {
        localResaults.Clear();
        string pattern = @"(?=(one|two|three|four|five|six|seven|eight|nine|\d))";
        var values = Regex.Matches(line, pattern);
        foreach (Match m in values)
        {
            localResaults.Add(Parser(m.Groups[1].Value));
        }
        total += Convert.ToInt32($"{localResaults.First()}{localResaults.Last()}");
    }
}
Console.WriteLine(total);
