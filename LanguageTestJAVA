import java.util.*;
import java.util.function.Function;
import java.util.stream.Collectors;
import java.util.regex.Pattern;
import java.util.regex.Matcher;

public class WordAnalyzer {
    public static List<Map.Entry<String, Long>> analyzeText(String text) {
        // 1. 전처리 및 스트림 생성
        List<String> words = new ArrayList<>();
        Matcher m = Pattern.compile("\\b\\w{4,}\\b").matcher(text.toLowerCase());
        
        while (m.find()) {
            words.add(m.group());
        }

        // 2. 빈도수 계산 및 정렬 (Stream API 활용)
        return words.stream()
            // 단어별 그룹화 및 카운팅
            .collect(Collectors.groupingBy(Function.identity(), Collectors.counting()))
            .entrySet().stream()
            // 정렬: 빈도 내림차순 -> 단어 오름차순
            .sorted(Map.Entry.<String, Long>comparingByValue(Comparator.reverseOrder())
                .thenComparing(Map.Entry.comparingByKey()))
            // 상위 3개 제한
            .limit(3)
            .collect(Collectors.toList());
    }

    public static void main(String[] args) {
        String inputText = "Coding is fun. Coding is powerful. Python coding is simple and powerful.";
        List<Map.Entry<String, Long>> result = analyzeText(inputText);
        System.out.println("Java Result: " + result);
    }
}
