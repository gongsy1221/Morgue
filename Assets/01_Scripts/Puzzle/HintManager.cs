using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    private string[] hints;
    private int currentHintIndex;

    void Start()
    {
        hints = new string[] {
            "첫 번째 힌트: 방 안을 잘 살펴보세요.",
            "두 번째 힌트: 책상 위에 단서가 있습니다.",
            "세 번째 힌트: 기어 장치를 조작해보세요."
        };
        currentHintIndex = 0;
        DisplayHint();
    }

    public void DisplayHint()
    {
        if (currentHintIndex < hints.Length)
        {
            hintText.text = hints[currentHintIndex];
            currentHintIndex++;
        }
    }
}
