using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerLookCheck : MonoBehaviour
{
    [SerializeField] GameObject ghostObj;

    public Transform ghostGirl;

    private Camera playerCamera;
    private Player _player;
    
    private float gazeTime = 2.0f;
    private float timer = 0.0f;
    private float ghostGirlY;
    public float moveDuration = 0.5f;
    public int moveSteps = 3;

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
                    StartCoroutine(MoveGhostGirlCoroutine());

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

    private IEnumerator MoveGhostGirlCoroutine()
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraPosition = playerCamera.transform.position;

        Vector3 startPosition = ghostGirl.position;
        Vector3 endPosition = cameraPosition + cameraForward;
        endPosition.y = ghostGirlY - 0.1f;

        for (int i = 1; i <= moveSteps; i++)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, (float)i / moveSteps);
            ghostGirl.position = newPosition;
            ghostGirl.LookAt(new Vector3(cameraPosition.x, ghostGirlY, cameraPosition.z));

            yield return new WaitForSeconds(moveDuration);
        }

        ghostGirl.gameObject.SetActive(false);
        ghostObj.SetActive(true);
    }

    private void RestartGame()
    {
        RoomManager.Instance.ReStartGame();
    }
}
