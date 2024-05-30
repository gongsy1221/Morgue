using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoSingleton<RoomManager>
{
    [SerializeField]private GameObject[] specialRoomPrefab;
    [SerializeField]private GameObject normalRoomPrefab;

    public GameObject currentRoom;
    private GameObject beforeRoom;

    public int roomNumber = 0;
    private const int maxRooms = 8;

    public bool isSpecial = false;

    // 플레이어가 복도를 지났는지 체크
    public void PlayerCheck(float x, float z, float angle = 0)
    {
        if (roomNumber <= maxRooms)
        {
            RandomRoom(x, z, angle);
        }
        else
        {
            // 게임 종료
            Debug.Log("End");
        }
    }

    // 맵 랜덤 생성
    private void RandomRoom(float x, float z, float angle = 0)
    {
        int rand = Random.Range(0, specialRoomPrefab.Length);
        if(rand == 0)
            isSpecial = false;
        else
            isSpecial = true;

        beforeRoom = currentRoom;
        currentRoom = Instantiate(isSpecial ? specialRoomPrefab[rand -1] : normalRoomPrefab, 
            new Vector3(beforeRoom.transform.position.x + x, 0, beforeRoom.transform.position.z + z), 
            Quaternion.Euler(0, beforeRoom.transform.rotation.y + angle, 0));

        // 생성되고 다시 못 돌아가게 문 닫아주기
    }

    // 방에 플레이어가 들어왔는지 체크
    public void CheckInRoom()
    {
        // 들어온 문 닫아주기
        // 전에 있던 방은 없애기
        Debug.Log("room");
        Destroy(beforeRoom);
        // Map 안에 있는 Street 활성화 하기
        currentRoom.transform.Find("Street").gameObject.SetActive(true);

        // 모든 작업 완료하면 Room Collider 비활성화
        GameObject.FindWithTag("Room").SetActive(false);
    }
}
