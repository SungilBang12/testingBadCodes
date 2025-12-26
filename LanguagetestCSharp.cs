using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static List<Tuple<string, int>> AnalyzeText(string text)
    {
        // 전처리: 4글자 이상의 단어 추출
        var cleanWords = Regex.Matches(text.ToLower(), @"\b\w{4,}\b")
                              .Cast<Match>()
                              .Select(m => m.Value)
                              .ToList();
        
        // 2. 빈도수 계산
        var wordCounts = new Dictionary<string, int>();
        foreach (var word in cleanWords)
        {
            if (wordCounts.ContainsKey(word))
                wordCounts[word]++;
            else
                wordCounts[word] = 1;
        }
        
        // 3. 정렬: 빈도수 기준 내림차순, 같다면 단어 알파벳순
        var sortedWords = wordCounts
            .OrderByDescending(item => item.Value)
            .ThenBy(item => item.Key)
            .ToList();
        
        // 4. 상위 3개 추출
        return sortedWords
            .Take(3)
            .Select(item => Tuple.Create(item.Key, item.Value))
            .ToList();
    }
    
    static void Main()
    {
        // 실행 예시
        string inputText = "Coding is fun. Coding is powerful. Python coding is simple and powerful.";
        var result = AnalyzeText(inputText);
        
        Console.Write("C# Result: [");
        for (int i = 0; i < result.Count; i++)
        {
            Console.Write($"('{result[i].Item1}', {result[i].Item2})");
            if (i < result.Count - 1)
                Console.Write(", ");
        }
        Console.WriteLine("]");
        // 예상 결과: [('coding', 3), ('powerful', 2), ('python', 1)]
    }
}
