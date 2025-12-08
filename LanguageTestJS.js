const analyzeText = (text) => {
    // 1. 전처리: 소문자 변환 및 정규식 매칭
    const words = text.toLowerCase().match(/\b\w{4,}\b/g) || [];

    // 2. 빈도수 계산 (reduce 사용)
    const wordCounts = words.reduce((acc, word) => {
        acc[word] = (acc[word] || 0) + 1;
        return acc;
    }, {});

    // 3. 배열 변환 및 정렬
    const sortedWords = Object.entries(wordCounts)
        .sort((a, b) => {
            // 빈도수 내림차순, 같다면 알파벳 오름차순
            if (b[1] === a[1]) return a[0].localeCompare(b[0]);
            return b[1] - a[1];
        });

    // 4. 상위 3개 추출
    return sortedWords.slice(0, 3);
};

// 실행 예시
const inputText = "Coding is fun. Coding is powerful. Python coding is simple and powerful.";
const result = analyzeText(inputText);
console.log("JS Result:", result);
// 예상 결과: [ [ 'coding', 3 ], [ 'powerful', 2 ], [ 'python', 1 ] ]
