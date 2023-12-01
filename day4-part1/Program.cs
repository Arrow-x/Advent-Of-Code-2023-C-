string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

string? line;
int total = 0;

using (var fs = File.OpenText(data))
{
    while ((line = fs.ReadLine()) != null)
    {
        line = line.Remove(0, 10);
        var stringSplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        var split = line.Split("|", stringSplitOptions);
        var winingSide = split[0].Split(" ", stringSplitOptions).Select(x => int.Parse(x)).ToArray<int>();
        var currentScratch = split[1].Split(" ", stringSplitOptions).Select(x => int.Parse(x)).ToArray<int>();
        int score = 0;
        foreach (var scratch in currentScratch)
        {
            foreach (var win in winingSide)
            {
                if (win == scratch)
                {
                    if (score == 0)
                    {
                        score = 1;
                    }
                    else
                    {
                        score *= 2;
                    }
                }
            }
        }
        total += score;
    }
}
Console.WriteLine(total);
