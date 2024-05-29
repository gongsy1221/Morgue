using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cabinet : Interactable
{
    [SerializeField] private GameObject cabinetPassword;
    [SerializeField] private GameObject cabienetDoor;
    private bool isSolved;
    
    public override void Interact()
    {
        cabinetPassword.SetActive(true);   
    }

    public override void Solved()
    {
        Close();
        StartCoroutine(OpenDoorRoutine());
    }

    IEnumerator OpenDoorRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        float x = cabienetDoor.transform.position.x;
        if(x >= 0)
        {
            x -= 0.01f;
            cabienetDoor.transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }

    public override void Close()
    {
        cabinetPassword.SetActive(false);
    }
}
