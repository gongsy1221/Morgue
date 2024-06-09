using UnityEngine;

public class PlayerLookCheck : MonoBehaviour
{
    public Camera playerCamera;
    public float gazeTime = 2.0f;

    private float timer = 0.0f;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Target"))
            {
                timer += Time.deltaTime;
                if (timer >= gazeTime)
                {
                    Debug.Log("Game Over");
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
}
