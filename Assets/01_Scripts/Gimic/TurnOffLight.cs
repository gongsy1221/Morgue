using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    [SerializeField] private GameObject lightObj;
    [SerializeField] private GameObject ghostObj;
    [SerializeField] private AudioClip audioClip;

    private Player _player;

    private int minTime = 3;
    private int maxTime = 6;
    private int randTime = 5;
    private float currentTime = 0f;

    private bool currentState = true;
    private bool checkState = true;

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
            SoundManager.StopFx(audioClip);
            StartCoroutine(ChangeLightObj());
            currentTime = 0f;
        }

        if(!checkState && _player.isMove)
        {
            _player.PlayerDie();
            ghostObj.SetActive(true);

            Invoke("RestartGame", 1f);
        }
    }

    private IEnumerator ChangeLightObj()
    {
        randTime = Random.Range(minTime, maxTime);
        currentState = !currentState;
        lightObj.SetActive(currentState);
        if (!currentState)
            SoundManager.PlayFx(audioClip, 1, true);

        if(checkState)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return null;
        }
        checkState = currentState;
    }

    private void RestartGame()
    {
        RoomManager.Instance.ReStartGame();
    }
}
