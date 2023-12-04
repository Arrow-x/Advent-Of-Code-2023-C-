string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;

string[] rawCards = File.ReadLines(data).ToArray<string>();
int[] cardsCounts = new int[rawCards.Length];
Array.Fill(cardsCounts, 1);

for (int cardIdx = 0; cardIdx < rawCards.Length; cardIdx++)
{
    var newScratchs = MangageGame(rawCards[cardIdx]);
    if (cardIdx + newScratchs < cardsCounts.Length)
    {
        for (int i = 1; i <= newScratchs; i++)
        {
            cardsCounts[cardIdx + i] += cardsCounts[cardIdx];
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
Console.WriteLine(cardsCounts.Sum());
