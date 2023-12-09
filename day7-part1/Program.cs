string data = Directory.GetCurrentDirectory() + @"/data/input";
if (File.Exists(data) == false)
    return;
var lines = File.ReadLines(data);
var splitOptions = StringSplitOptions.TrimEntries;

var highCard = new List<(int[] card, int win)>();
var onePair = new List<(int[] card, int win)>();
var twoPair = new List<(int[] card, int win)>();
var threeOfKind = new List<(int[] card, int win)>();
var fullHouse = new List<(int[] card, int win)>();
var fourOfKind = new List<(int[] card, int win)>();
var fiveOfKind = new List<(int[] card, int win)>();

var indexesChecked = new List<int>();
foreach (var line in lines) {
    var split = line.Split(" ", splitOptions);
    var rawCard = split[0];
    var wining = int.Parse(split[1]);
    var valueCard = new int[5];
    int pairs = 0;
    int threes = 0;
    int fours = 0;
    int fives = 0;
    indexesChecked.Clear();
    for (int outerI = 0; outerI < rawCard.Length; outerI++) {
        valueCard[outerI] = rawCard[outerI] switch {
            '2' => 1,  '3' => 2,  '4' => 3,  '5' => 4, '6' => 5,
            '7' => 6,  '8' => 7,  '9' => 8,  'T' => 9, 'J' => 10,
            'Q' => 11, 'K' => 12, 'A' => 13,
            _ => 0,
        };
        if (indexesChecked.Contains(outerI)) {
            continue;
        }
        int kind = 0;
        for (int innerI = 0; innerI < rawCard.Length; innerI++) {
            if (outerI == innerI | indexesChecked.Contains(innerI)) {
                continue;
            }
            if (rawCard[outerI] == rawCard[innerI]) {
                kind += 1;
                indexesChecked.Add(innerI);
                indexesChecked.Add(outerI);
            }
        }
        switch (kind) {
            case 0:
                break;
            case 1:
                pairs++;
                break;
            case 2:
                threes++;
                break;
            case 3:
                fours++;
                break;
            case 4:
                fives++;
                break;
        }
    }
    var resault = (valueCard, wining);
    if (pairs == 0 && threes == 0 && fours == 0 && fives == 0) {
        AddToList(resault, highCard);
    } else if (pairs == 1 && threes == 0) {
        AddToList(resault, onePair);
    } else if (pairs == 2 && threes == 0) {
        AddToList(resault, twoPair);
    } else if (pairs == 0 && threes == 1) {
        AddToList(resault, threeOfKind);
    } else if (pairs == 1 && threes == 1) {
        AddToList(resault, fullHouse);
    } else if (fours == 1) {
        AddToList(resault, fourOfKind);
    } else if (fives == 1) {
        AddToList(resault, fiveOfKind);
    }
}
bool AddToList((int[] card, int win)input, List<(int[] card, int win)> list,
               int cardIdx = 0, int start = 0) {
    if (list.Count == 0) {
        list.Add(input);
        return true;
    }
    for (int listIdx = start; listIdx < list.Count; listIdx++) {
        if (cardIdx > 0) {
            if (input.card[cardIdx - 1] != list[listIdx].card[cardIdx - 1]) {
                return false;
            }
        }
        if (input.card[cardIdx] < list[listIdx].card[cardIdx]) {
            continue;
        } else if (input.card[cardIdx] > list[listIdx].card[cardIdx]) {
            list.Insert(listIdx, input);
            return true;
        } else if (input.card[cardIdx] == list[listIdx].card[cardIdx]) {
            if (cardIdx + 1 < input.card.Length) {
                var suc = AddToList(input, list, cardIdx + 1, listIdx);
                if (suc == true) {
                    return true;
                }
            }
        }
    }
    list.Add(input);
    return true;
}
var cardWinTypes =
    new List<(int[] card, int win)>[7] { highCard,    onePair,   twoPair,
                                         threeOfKind, fullHouse, fourOfKind,
                                         fiveOfKind };
// Console.WriteLine($@"High Cards: {highCard.Count}, One Pair {onePair.Count},
// two Pairs {twoPair.Count}, Three Of Kind {threeOfKind.Count}, Full House:
// {fullHouse.Count}, Four Of Kind {fourOfKind.Count}, Five Of Kind
// {fiveOfKind.Count}");
int score = 0;
int rankIdx = 1;
foreach (var type in cardWinTypes) {
    for (int card = type.Count - 1; card >= 0; card--) {
        var _currentWin = type[card].win;
        // Console.WriteLine(
        //     $"{{type[card].win} * {rankIdx}");
        score += rankIdx * type[card].win;
        rankIdx++;
    }
}
Console.WriteLine(score);
