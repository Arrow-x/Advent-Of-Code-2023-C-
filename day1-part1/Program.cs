using System.Text.RegularExpressions;

string data = Directory.GetCurrentDirectory() + @"/data/test";
if (File.Exists(data) == false) return;

string? line;
int total = 0;

using (var fs = File.OpenText(data))
{
    var localResaults = new List<int>();
    while ((line = fs.ReadLine()) != null)
    {
        localResaults.Clear();
        Regex rx = new Regex(@"\d", RegexOptions.Compiled);
        var matched = rx.Matches(line);
        foreach (Match match in matched)
        {
            var num = int.TryParse(match.ValueSpan, out int res);
            localResaults.Add(res);
        }
        total += Convert.ToInt32($"{localResaults.First()}{localResaults.Last()}");
    }
}
Console.WriteLine(total);
