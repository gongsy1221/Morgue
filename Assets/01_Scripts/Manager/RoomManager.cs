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

    // �÷��̾ ������ �������� üũ
    public void PlayerCheck(float x, float z, float angle = 0)
    {
        if (roomNumber <= maxRooms)
        {
            RandomRoom(x, z, angle);
        }
        else
        {
            // ���� ����
            Debug.Log("End");
        }
    }

    // �� ���� ����
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

        // �����ǰ� �ٽ� �� ���ư��� �� �ݾ��ֱ�
    }

    // �濡 �÷��̾ ���Դ��� üũ
    public void CheckInRoom()
    {
        // ���� �� �ݾ��ֱ�
        // ���� �ִ� ���� ���ֱ�
        Debug.Log("room");
        Destroy(beforeRoom);
        // Map �ȿ� �ִ� Street Ȱ��ȭ �ϱ�
        currentRoom.transform.Find("Street").gameObject.SetActive(true);

        // ��� �۾� �Ϸ��ϸ� Room Collider ��Ȱ��ȭ
        GameObject.FindWithTag("Room").SetActive(false);
    }
}
