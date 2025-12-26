using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    // Magic Number 제거: 상수로 정의
    private const int MIN_WORD_LENGTH = 4;
    private const int TOP_RESULT_COUNT = 3;
    
    static List<Tuple<string, int>> AnalyzeText(string text)
    {
        // 1. 전처리: 소문자 변환 및 정규표현식으로 단어만 추출
        // \b\w{4,}\b : 4글자 이상의 단어 경계 매칭
        string pattern = $@"\b\w{{{MIN_WORD_LENGTH},}}\b";
        var cleanWords = Regex.Matches(text.ToLower(), pattern)
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
            .Take(TOP_RESULT_COUNT)
            .Select(item => Tuple.Create(item.Key, item.Value))
            .ToList();
    }
    
    static void Main()
    {
        // 실행 예시
        string inputText = "Coding is fun. Coding is powerful. Python coding is simple and powerful.";
        var result = AnalyzeText(inputText);
        
        // StringBuilder 사용으로 문자열 연결 최적화
        var sb = new StringBuilder();
        sb.Append("C# Result: [");
        
        for (int i = 0; i < result.Count; i++)
        {
            sb.Append($"('{result[i].Item1}', {result[i].Item2})");
            if (i < result.Count - 1)
                sb.Append(", ");
        }
        sb.Append("]");
        
        Console.WriteLine(sb.ToString());
        // 예상 결과: [('coding', 3), ('powerful', 2), ('python', 1)]
    }
}
