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
            "ù ��° ��Ʈ: �� ���� �� ���캸����.",
            "�� ��° ��Ʈ: å�� ���� �ܼ��� �ֽ��ϴ�.",
            "�� ��° ��Ʈ: ��� ��ġ�� �����غ�����."
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
