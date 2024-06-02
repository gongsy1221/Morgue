using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cabinet : Interactable
{
    public bool open = false;
    public float cabinetOpenPos = -0.1f;
    public float cabinetClosePos = 0.3f;
    public float smoot = 2f;

    public override void Interact()
    {
        open = !open;
    }

    void Update()
    {
        if (open)
        {
            Vector3 targetPos = new Vector3(cabinetOpenPos, transform.position.y, 0.12f);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoot * Time.deltaTime);
        }
        else
        {
            Vector3 targetPos = new Vector3(cabinetClosePos, transform.position.y, 0.12f);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoot * Time.deltaTime);
        }
    }

    public override void Solved()
    {
    }
}
