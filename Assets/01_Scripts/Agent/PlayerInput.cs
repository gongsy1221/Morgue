using System;
using TMPro;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

interface IInteractable
{
    public void Interact();
}

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> MovementEvent;
    public event Action JumpEvent;
    
    public Vector3 MousePosition { get;private set; }

    public LayerMask interactableLayer;
    private float interactDistance = 4f;

    private bool _playerInputEnabled = true;

    public void SetPlayerInput(bool enable)
    {
        _playerInputEnabled = enable;
    }

    private void Update()
    {
        if (_playerInputEnabled == false) return;
        CheckJumpInput();
        CheckMoveInput();
        CheckInteractInput();
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpEvent?.Invoke();
        }
    }

    private void CheckMoveInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = new Vector3(horizontal, 0, vertical);

        MovementEvent?.Invoke(moveVector.normalized);
    }

    private void CheckInteractInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}