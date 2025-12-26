using System;
using System.Collections.Generic; // List, KeyValuePair, Dictionary 등
using System.Linq;                // Stream API에 대응하는 LINQ 기능
using System.Text.RegularExpressions; // Regex

public class WordAnalyzer
{
    public static List<KeyValuePair<string, long>> AnalyzeText(string text)
    {
        // 1. 전처리 및 정규식 매칭 (Java의 Pattern/Matcher 대응)
        // C#의 Regex.Matches는 MatchCollection을 반환하며, 이는 열거 가능(Enumerable)합니다.
        // \b\w{4,}\b : 단어 경계가 있고 4글자 이상인 단어 추출
        var matches = Regex.Matches(text.ToLower(), @"\b\w{4,}\b");

        // Java에서는 while loop로 List를 만들고 stream을 열었지만,
        // C#에서는 LINQ를 사용하여 매칭 결과에서 바로 흐름을 이어갈 수 있습니다.
        return matches.Cast<Match>() // MatchCollection을 IEnumerable<Match>로 변환
            .Select(m => m.Value)    // 매치된 문자열(단어)만 추출
            
            // 2. 빈도수 계산 (Java: collect(groupingBy...))
            .GroupBy(word => word)   // 단어별로 그룹화
            .Select(g => new KeyValuePair<string, long>(g.Key, g.LongCount())) // (단어, 횟수) 구조체 생성
            
            // 3. 정렬 (Java: sorted + Comparator)
            .OrderByDescending(kv => kv.Value) // 빈도 내림차순
            .ThenBy(kv => kv.Key)              // 단어 오름차순 (동점일 경우)
            
            // 4. 상위 3개 제한 및 리스트 반환
            .Take(3)
            .ToList();
    }

    public static void Main(string[] args)
    {
        string inputText = "Coding is fun. Coding is powerful. Python coding is simple and powerful.";
        
        List<KeyValuePair<string, long>> result = AnalyzeText(inputText);

        // 결과 출력 (Java의 Map.Entry 출력 포맷 'key=value'를 흉내 내어 출력)
        // C#의 KeyValuePair.ToString()은 '[key, value]' 형식이므로 보기 좋게 포맷팅합니다.
        string formattedResult = string.Join(", ", result.Select(kv => $"{kv.Key}={kv.Value}"));
        Console.WriteLine($"C# Result: [{formattedResult}]");
    }
}
