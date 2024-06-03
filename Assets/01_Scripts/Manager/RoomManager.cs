using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoSingleton<RoomManager>
{
    [SerializeField]private GameObject[] specialRoomPrefab;
    [SerializeField]private GameObject normalRoomPrefab;

    public Transform forwardMap;
    public Transform backwardMap;
    public GameObject currentRoom;
    private GameObject beforeRoom;

    public int roomNumber = 0;
    private const int maxRooms = 8;

    public bool isSpecial = false;
    public bool isRoom = false;
    public bool isForward = true;

    private void Start()
    {
        isRoom = false;
        isSpecial = false;

        Initialize();
    }

    private void Initialize()
    {
        forwardMap = GameObject.FindWithTag("FMap").transform;
        backwardMap = GameObject.FindWithTag("BMap").transform;

        KeySpawner.Instance.spawnPoints = GameObject.FindWithTag("Point").GetComponentsInChildren<Transform>();
        KeySpawner.Instance.RandomSpawnKey();
    }

    // 플레이어가 복도를 지났는지 체크
    public void PlayerCheck()
    {
        if (roomNumber <= maxRooms)
        {
            StartCoroutine(RandomRoom());
        }
        else
        {
            // 게임 종료
        }
    }

    private IEnumerator RandomRoom()
    {
        isRoom = true;

        int rand = Random.Range(0, specialRoomPrefab.Length);
        isSpecial = rand != 0;

        beforeRoom = currentRoom;

        yield return null;

        if (isForward)
        {
            currentRoom = Instantiate(isSpecial ? specialRoomPrefab[rand - 1] : normalRoomPrefab,
                                      forwardMap.position, forwardMap.rotation);
        }
        else
        {
            currentRoom = Instantiate(isSpecial ? specialRoomPrefab[rand - 1] : normalRoomPrefab,
                                      backwardMap.position, backwardMap.rotation);
        }

        yield return null;

        // 맵이 생성되고 다시 못 돌아가게 문 닫아주기
        if (isForward)
            beforeRoom.transform.Find("FrontDoor").GetComponent<Door>().CloseDoor();
        else
            beforeRoom.transform.Find("BackDoor").GetComponent<Door>().CloseDoor();
        
        yield return null;
    }

    // 방에 플레이어가 들어왔는지 체크
    public void CheckInRoom()
    {
        isRoom = false;

        Destroy(beforeRoom);
        currentRoom.transform.Find("Street").gameObject.SetActive(true);
        currentRoom.transform.Find("BackDoor").GetComponent<Door>().CloseDoor();
        TimerManager.Instance.StartTimer();
        Invoke("Initialize", 0.5f);

        GameObject.FindWithTag("Room").SetActive(false);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
        StartCoroutine(FadeManager.Instance.FadeIn());
    }
}
