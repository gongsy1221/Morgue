using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDoor : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        SoundManager.PlayFx(audioClip, 1, true);
    }

    private void Update()
    {
        if(RoomManager.Instance.isRoom)
            SoundManager.StopFx(audioClip);
    }
}
