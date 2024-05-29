using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBook : Interactable
{
    [SerializeField] private GameObject notebook;

    public override void Interact()
    {
        notebook.SetActive(true);
    }

    public override void Solved()
    {

    }

    public override void Close()
    {
        notebook.SetActive(false);
    }
}
