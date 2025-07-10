using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour , IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    private void Awake()
    {
        DeSelect();
    }
    public void Select()
    {
       image.color = selectedColor;
    }
    public void DeSelect()
    {
       image.color = notSelectedColor;
    }
    //drop function
    public void OnDrop(PointerEventData eventData)
    {
        // check if there is an item
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }


    }

}
