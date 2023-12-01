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

    string? numBlock = "";
    bool isEnginePart = false;
    for (int charIdx = 0; charIdx < currentLine.Length; charIdx++)
    {
        if (Char.IsDigit(currentLine[charIdx]) == true)
        {
            if (isEnginePart == false)
            {
                if (CheckAround(ref charIdx, ref currentLine, ref nextLine, ref lastLine) == true)
                {
                    isEnginePart = true;
                }
            }
            numBlock += currentLine[charIdx];
        }
        else
        {
            CheckIfEngine(ref numBlock, ref isEnginePart);
        }
        if (charIdx == currentLine.Length - 1)
        {
            CheckIfEngine(ref numBlock, ref isEnginePart);
        }
    }
    lastLine = "";
    currentLine = "";
    nextLine = "";

}

bool CheckSymbol(Char symbol)
{
    if (symbol != '.' && Char.IsDigit(symbol) != true)
    {
        return true;
    }
    return false;
}

bool CheckAround(ref int charIdx, ref string currentLine, ref string nextLine, ref string lastLine)
{
    if (lastLine != "")
    {
        if (CheckSymbol(lastLine[charIdx]) == true)
        {
            return true;
        }
    }
    if (nextLine != "")
    {
        if (CheckSymbol(nextLine[charIdx]) == true)
        {
            return true;
        }
    }
    if (charIdx + 1 < currentLine.Length)
    {
        if (CheckSymbol(currentLine[charIdx + 1]) == true)
        {
            return true;
        }
        if (lastLine != "")
        {
            if (CheckSymbol(lastLine[charIdx + 1]) == true)
            {
                return true;
            }
        }
        if (nextLine != "")
        {
            if (CheckSymbol(nextLine[charIdx + 1]) == true)
            {
                return true;
            }
        }
    }
    if (charIdx - 1 >= 0)
    {
        if (CheckSymbol(currentLine[charIdx - 1]) == true)
        {
            return true;
        }
        if (lastLine != "")
        {
            if (CheckSymbol(lastLine[charIdx - 1]) == true)
            {
                return true;
            }
        }
        if (nextLine != "")
        {
            if (CheckSymbol(nextLine[charIdx - 1]) == true)
            {
                return true;
            }
        }
    }
    return false;
}

void CheckIfEngine(ref string numBlock, ref bool isEnginePart)
{
    if (numBlock != "")
    {
        if (isEnginePart == true)
        {
            isEnginePart = false;
            total += int.Parse(numBlock);
        }
        numBlock = "";
    }
}

Console.WriteLine(total);
