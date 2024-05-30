using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2f;

    public override void Interact()
    {
        open = !open;
    }

    void Update()
    {
        if (open)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.localRotation.x, doorOpenAngle, transform.localRotation.y);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(transform.localRotation.x, doorCloseAngle, transform.localRotation.z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }
    }

    public override void Solved()
    {
    }

    public override void Close()
    {
    }

}
