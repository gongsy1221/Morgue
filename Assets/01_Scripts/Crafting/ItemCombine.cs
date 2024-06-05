using UnityEngine;

[CreateAssetMenu(fileName = "New Combine Item", menuName = "Inventory/CombineItem")]
public class ItemCombine : ScriptableObject
{
    public Item item1;
    public Item item2;
    public Item resultItem;
}
