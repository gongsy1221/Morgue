using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    [SerializeField] GameObject lightObj;
    [SerializeField] GameObject ghostObj;

    private Player _player;

    private int minTime = 3;
    private int maxTime = 6;
    private int randTime = 5;
    private float currentTime = 0f;

    private bool currentState = true;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        ghostObj = _player.transform.Find("PlayerDie").gameObject;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > randTime)
        {
            ChangeLightObj();
            currentTime = 0f;
        }

        if(!currentState && _player.isMove)
        {
            _player.PlayerDie();
            ghostObj.SetActive(true);

            Invoke("RestartGame", 1.5f);
        }
    }

    private void ChangeLightObj()
    {
        randTime = Random.Range(minTime, maxTime);
        currentState = !currentState;
        lightObj.SetActive(currentState);
    }

    private void RestartGame()
    {
        RoomManager.Instance.ReStartGame();
    }
}
