using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RisingWater : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;

    private Player _player;

    private float currentY = -0.75f;
    private float maxY = 0.8f;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (door1.isKey || door2.isKey) return;

        if (currentY <= maxY)
        {
            currentY += 0.001f;
            water.transform.position =
                    new Vector3(water.transform.position.x, currentY, water.transform.position.z);
        }
        else
        {
            _player.PlayerDie();
            RestartGame();
        }
    }

    private void RestartGame()
    {
        RoomManager.Instance.ReStartGame();
    }
}
