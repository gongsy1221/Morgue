using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private int ultimateTime;

    public bool isTimer = false;
    private float currentTime;

    private void Start()
    {
        currentTime = 0;
        timeText.text = ultimateTime.ToString() + "초";
    }

    private void Update()
    {
        if(isTimer)
        {
            currentTime += Time.deltaTime;
            timeText.text = (ultimateTime - (int)currentTime).ToString() + "초";
            if (currentTime > ultimateTime)
            {
                timeText.text = "시간 초과!";
                StartCoroutine(FadeManager.Instance.FadeIn());
                RoomManager.Instance.ReStartGame();
            }
        }
    }

    public void StartTimer()
    {
        isTimer = true;
    }

    public void StopTimer()
    {
        isTimer = false;
        currentTime = 0;
    }
}
