using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerLookCheck : MonoBehaviour
{
    [SerializeField] GameObject ghostObj;

    public GameObject ghostGirl;

    private Camera playerCamera;
    private Player _player;
    
    private float gazeTime = 2.0f;
    private float timer = 0.0f;
    private float ghostGirlY;

    private void Awake()
    {
        playerCamera = Camera.main;
        _player = FindObjectOfType<Player>();
        ghostObj = _player.transform.Find("PlayerDie").gameObject;

        if (ghostGirl != null)
            ghostGirlY = ghostGirl.transform.position.y;
    }

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= gazeTime)
                {
                    _player.PlayerDie();
                    MoveGhostGirlInFrontOfPlayer();
                    ghostObj.SetActive(true);

                    Invoke("RestartGame", 0.8f);
                }
            }
            else
            {
                timer = 0.0f;
            }
        }
        else
        {
            timer = 0.0f;
        }
    }

    private void MoveGhostGirlInFrontOfPlayer()
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraPosition = playerCamera.transform.position;

        Vector3 newPosition = cameraPosition + cameraForward * 0.8f;
        newPosition.y = ghostGirlY - 0.1f;
        ghostGirl.transform.position = newPosition;

        ghostGirl.transform.LookAt(new Vector3(cameraPosition.x, ghostGirlY, cameraPosition.z));
    }

    private void RestartGame()
    {
        RoomManager.Instance.ReStartGame();
    }
}
