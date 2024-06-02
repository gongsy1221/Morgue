using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public Item key;

    public override void Interact()
    {
        Inventory.Instance.Add(key);
        Destroy(gameObject);
    }

    public override void Solved()
    {
    }
}
