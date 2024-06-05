using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    private Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public Item GetItem()
    {
        return item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            icon.transform.SetParent(transform.root);
            icon.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            icon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;
            if (hitObject != null)
            {
                InventorySlot targetSlot = hitObject.GetComponent<InventorySlot>();
                if (targetSlot != null)
                {
                    // 아이템 조합 로직 추가
                    CombineItems(targetSlot.GetItem());
                }
            }

            icon.transform.SetParent(transform);
            icon.transform.localPosition = Vector3.zero;
        }
    }

    private void CombineItems(Item targetItem)
    {
        ItemCombine result = CombinationManager.Instance.Combine(item, targetItem);
        if (result != null)
        {
            Inventory.Instance.Remove(item);
            Inventory.Instance.Remove(targetItem);
            Inventory.Instance.Add(result.resultItem);
            Debug.Log("Items combined to create: " + result.resultItem.itemName);
        }
    }
}
