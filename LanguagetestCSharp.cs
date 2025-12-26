using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static List<Tuple<string, int>> AnalyzeText(string text)
    {
        // 1. 전처리: 소문자 변환 및 정규표현식으로 단어만 추출
        // \b\w{4,}\b : 4글자 이상의 단어 경계 매칭
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
```

Python 코드와 동일한 로직을 C#으로 구현했습니다:

**주요 변환 내용:**
1. **정규표현식**: `Regex.Matches()`로 패턴 매칭
2. **빈도수 계산**: `Dictionary<string, int>`로 단어별 카운트
3. **정렬**: LINQ의 `OrderByDescending()`과 `ThenBy()`로 다중 조건 정렬
4. **결과 형식**: Python의 튜플 리스트와 동일하게 `Tuple<string, int>` 사용

**실행 결과:**
```
C# Result: [('coding', 3), ('powerful', 2), ('python', 1)]
