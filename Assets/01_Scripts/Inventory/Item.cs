using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "SO/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
}