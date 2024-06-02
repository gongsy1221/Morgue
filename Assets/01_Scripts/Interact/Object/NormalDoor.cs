using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NormalDoor : Interactable
{
    public bool isOpen = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2f;

    public override void Interact()
    {
        isOpen = !isOpen;
    }

    private void Update()
    {
        if (isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.x, doorOpenAngle, transform.localRotation.y);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.x, doorCloseAngle, transform.localRotation.z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
    }

    public override void Solved()
    {
        
    }
}
