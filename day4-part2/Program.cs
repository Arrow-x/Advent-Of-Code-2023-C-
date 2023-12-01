string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

string? line;
int total = 0;
var cards = new List<List<string>>();

using (var fs = File.OpenText(data))
{
    while ((line = fs.ReadLine()) != null)
    {
        var newLine = new List<string>() { line };
        cards.Add(newLine);
    }
}
for (int cardIdx = 0; cardIdx < cards.Count; cardIdx++)
{
    foreach (var card in cards[cardIdx])
    {
        var newScratchs = MangageGame(card);
        if (cardIdx + newScratchs < cards.Count)
        {
            for (int i = 1; i <= newScratchs; i++)
            {
                cards[cardIdx + i].Add(cards[cardIdx + i][0]);
            }
        }
    }
}

int MangageGame(string inputLine)
{
    int wins = 0;
    var line = inputLine.Remove(0, 10);
    var stringSplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
    var split = line.Split("|", stringSplitOptions);
    var winingSide = split[0].Split(" ", stringSplitOptions).Select(x => int.Parse(x)).ToArray<int>();
    var currentScratch = split[1].Split(" ", stringSplitOptions).Select(x => int.Parse(x)).ToArray<int>();
    foreach (var scratch in currentScratch)
    {
        foreach (var win in winingSide)
        {
            if (scratch == win)
            {
                wins++;
            }
        }
    }
    return wins;
}
foreach (var c in cards)
{
    total += c.Count;
}
Console.WriteLine(total);
