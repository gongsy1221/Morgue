using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : MonoSingleton<CombinationManager>
{
    public List<ItemCombine> combinations;

    public ItemCombine Combine(Item item1, Item item2)
    {
        foreach (ItemCombine combine in combinations)
        {
            if ((combine.item1 == item1 && combine.item2 == item2) || (combine.item1 == item2 && combine.item2 == item1))
            {
                return combine;
            }
        }
        return null;
    }
}
