string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false) return;
string? line;
var input = new List<string>();
using (var fs = File.OpenText(data))
{
    while ((line = fs.ReadLine()) != null)
    {
        input.Add(line);
    }
}

int total = 0;
string lastLine = "";
string currentLine = "";
string nextLine = "";

for (int lineIdx = 0; lineIdx < input.Count; lineIdx++)
{
    currentLine = input[lineIdx];
    if (lineIdx - 1 >= 0)
    {
        lastLine = input[lineIdx - 1];
    }
    if (lineIdx + 1 < input.Count)
    {
        nextLine = input[lineIdx + 1];
    }

    for (int charIdx = 0; charIdx < currentLine.Length; charIdx++)
    {
        int connectedPartsScore = 1;
        if (currentLine[charIdx] == '*')
        {
            var founds = CheckAround(ref charIdx, ref currentLine, ref nextLine, ref lastLine);
            if (founds.Count > 1)
            {
                foreach (var m in founds)
                {
                    connectedPartsScore = connectedPartsScore * int.Parse(m);
                }
            }
        }
        if (connectedPartsScore != 1)
        {
            total += connectedPartsScore;
        }
        connectedPartsScore = 1;
    }
    lastLine = "";
    currentLine = "";
    nextLine = "";
}

bool CheckSymbol(Char symbol)
{
    if (Char.IsDigit(symbol) == true)
    {
        return true;
    }
    return false;
}
string CheckNumbers(int idx, ref string line)
{
    string foundNumber = "";
    foundNumber += line[idx];
    for (int index = idx + 1; index < line.Length; index++)
    {
        if (CheckSymbol(line[index]) == false)
        {
            break;
        }
        foundNumber += line[index];

    }
    for (int index = idx - 1; index >= 0; index--)
    {
        if (CheckSymbol(line[index]) == false)
        {
            break;
        }
        foundNumber = foundNumber.Insert(0, line[index].ToString());
    }
    return foundNumber;
}
List<string> CheckAround(ref int charIdx, ref string currentLine, ref string nextLine, ref string lastLine)
{
    var foundNumbers = new List<string>();
    if (lastLine != "")
    {
        if (charIdx - 1 >= 0)
        {
            if (CheckSymbol(lastLine[charIdx - 1]) == true)
            {
                foundNumbers.Add(CheckNumbers(charIdx - 1, ref lastLine));
            }
        }
        if (CheckSymbol(lastLine[charIdx]) == true)
        {
            foundNumbers.Add(CheckNumbers(charIdx, ref lastLine));
        }
        if (charIdx + 1 < lastLine.Length)
        {
            if (CheckSymbol(lastLine[charIdx + 1]) == true)
            {
                foundNumbers.Add(CheckNumbers(charIdx + 1, ref lastLine));
            }
        }
    }
    if (nextLine != "")
    {
        if (charIdx - 1 >= 0)
        {
            if (CheckSymbol(nextLine[charIdx - 1]) == true)
            {
                foundNumbers.Add(CheckNumbers(charIdx - 1, ref nextLine));
            }
        }
        if (CheckSymbol(nextLine[charIdx]) == true)
        {
            foundNumbers.Add(CheckNumbers(charIdx, ref nextLine));
        }
        if (charIdx + 1 < nextLine.Length)
        {
            if (CheckSymbol(nextLine[charIdx + 1]) == true)
            {
                foundNumbers.Add(CheckNumbers(charIdx + 1, ref nextLine));
            }
        }
    }
    if (charIdx + 1 < nextLine.Length)
    {
        if (CheckSymbol(currentLine[charIdx + 1]) == true)
        {
            foundNumbers.Add(CheckNumbers(charIdx + 1, ref currentLine));
        }

    }
    if (charIdx - 1 >= 0)
    {
        if (CheckSymbol(currentLine[charIdx - 1]) == true)
        {
            foundNumbers.Add(CheckNumbers(charIdx - 1, ref currentLine));
        }

    }
    foundNumbers = foundNumbers.Distinct().ToList();
    return foundNumbers;
}
Console.WriteLine(total);
