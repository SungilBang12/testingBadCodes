import re

def analyze_text(text):
    # 1. 전처리: 소문자 변환 및 정규표현식으로 단어만 추출
    # \b\w{4,}\b : 4글자 이상의 단어 경계 매칭
    clean_words = re.findall(r'\b\w{4,}\b', text.lower())
    
    # 2. 빈도수 계산
    word_counts = {}
    for word in clean_words:
        word_counts[word] = word_counts.get(word, 0) + 1
        
    # 3. 정렬: 빈도수(value) 기준 내림차순, 같다면 단어(key) 알파벳순
    sorted_words = sorted(
        word_counts.items(), 
        key=lambda item: (-item[1], item[0])
    )
    
    # 4. 상위 3개 추출
    return sorted_words[:3]

# 실행 예시
input_text = "Coding is fun. Coding is powerful. Python coding is simple and powerful."
result = analyze_text(input_text)
print(f"Python Result: {result}")
# 예상 결과: [('coding', 3), ('powerful', 2), ('python', 1)]
