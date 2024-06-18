using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoSingleton<RoomManager>
{
    [SerializeField] private GameObject[] specialRoomPrefabArray;
    private List<GameObject> specialRoomPrefabList;
    [SerializeField] private GameObject normalRoomPrefab;
    [SerializeField] private GameObject lastRoom;

    public Transform forwardMap;
    public Transform backwardMap;
    public GameObject currentRoom;
    private GameObject beforeRoom;

    private GameObject roomNumObj;
    public Sprite[] roomNumSprite;

    public int roomNumber = 0;
    private const int maxRooms = 8;

    public bool isSpecial = false;
    public bool isRoom = false;
    public bool isForward = true;

    private void Start()
    {
        isRoom = false;
        isSpecial = false;

        specialRoomPrefabList = new List<GameObject>(specialRoomPrefabArray);

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
            GameOver();
        }
    }

    private void GameOver()
    {
        Instantiate(lastRoom, isForward ? forwardMap.position : backwardMap.position, isForward ?
                forwardMap.rotation : backwardMap.rotation);
    }

    private IEnumerator RandomRoom()
    {
        isRoom = true;

        if (specialRoomPrefabList.Count == 0 && !isSpecial)
        {
            currentRoom = Instantiate(normalRoomPrefab, isForward ? forwardMap.position : backwardMap.position, isForward ?
                forwardMap.rotation : backwardMap.rotation);
            yield return null;
        }

        int rand = Random.Range(0, specialRoomPrefabList.Count + 1);
        isSpecial = rand != 0;

        beforeRoom = currentRoom;

        yield return null;

        if (isSpecial)
        {
            int specialIndex = rand - 1;
            currentRoom = Instantiate(specialRoomPrefabList[specialIndex], isForward ? forwardMap.position : backwardMap.position,
                isForward ? forwardMap.rotation : backwardMap.rotation);
            specialRoomPrefabList.RemoveAt(specialIndex);
        }
        else
        {
            currentRoom = Instantiate(normalRoomPrefab, isForward ? forwardMap.position : backwardMap.position,
                isForward ? forwardMap.rotation : backwardMap.rotation);
        }

        //roomNumObj = FindObjectOfType

        yield return null;

        if (isForward)
            beforeRoom.transform.Find("FrontDoor").GetComponent<Door>().CloseDoor();
        else
            beforeRoom.transform.Find("BackDoor").GetComponent<Door>().CloseDoor();

        yield return null;
    }

    public bool ProcessRoomTransition(string tag)
    {
        bool transitionOccurred = false;

        if (tag == "Backward")
        {
            roomNumber = isSpecial ? roomNumber + 1 : 1;
            isForward = false;
            transitionOccurred = true;
        }
        else if (tag == "Forward")
        {
            roomNumber = isSpecial ? 1 : roomNumber + 1;
            isForward = true;
            transitionOccurred = true;
        }

        if (transitionOccurred && !isRoom)
        {
            PlayerCheck();
        }

        return transitionOccurred;
    }

    // 방에 플레이어가 들어왔는지 체크
    public void CheckInRoom()
    {
        isRoom = false;

        Destroy(beforeRoom);
        currentRoom.transform.Find("Street").gameObject.SetActive(true);

        Transform mapEvent = currentRoom.transform.Find("MapEvent");
        if (mapEvent != null)
        {
            mapEvent.gameObject.SetActive(true);
        }

        currentRoom.transform.Find("BackDoor").GetComponent<Door>().CloseDoor();
        //TimerManager.Instance.StartTimer();
        Invoke("Initialize", 0.5f);

        GameObject.FindWithTag("Room").SetActive(false);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("01_MainScene");
        StartCoroutine(FadeManager.Instance.FadeIn());
    }
}
