using UnityEngine;

public class PlayerLookCheck : MonoBehaviour
{
    public GameObject ghostGirl;

    private Camera playerCamera;
    private Player player;
    
    private float gazeTime = 2.0f;
    private float timer = 0.0f;
    private float ghostGirlY;

    private void Awake()
    {
        playerCamera = Camera.main;
        player = FindObjectOfType<Player>();

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
                    player.PlayerDie();
                    MoveGhostGirlInFrontOfPlayer();
                    Invoke("RestartGame", 1f);
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

        // ghostGirl을 플레이어 카메라 앞 1미터 지점으로 이동
        Vector3 newPosition = cameraPosition + cameraForward * 0.5f;
        newPosition.y = ghostGirlY - 0.1f;
        ghostGirl.transform.position = newPosition;

        // ghostGirl이 플레이어를 정확히 바라보도록 회전
        ghostGirl.transform.LookAt(new Vector3(cameraPosition.x, ghostGirlY, cameraPosition.z));
    }

    private void RestartGame()
    {
        RoomManager.Instance.ReStartGame();
    }
}
