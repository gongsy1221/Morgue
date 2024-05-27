using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePuzzle : MonoBehaviour, IInteractable
{
    public GameObject puzzleUI;
    private bool isSolved = false;

    public void Interact()
    {
        if (!isSolved)
        {
            puzzleUI.SetActive(true);
            
        }
    }

    public void SolvePuzzle()
    {
        isSolved = true;
        puzzleUI.SetActive(false);
    }
}
