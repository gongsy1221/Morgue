using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactText;
    public void UpdateText(string interact)
    {
        _interactText.text = interact;
    }
}
