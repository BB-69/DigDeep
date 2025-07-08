using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler 
{

    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    [HideInInspector] public DropItem dropItem;


    public void InitialiseItem(DropItem newItem)
    {
        dropItem = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
    }
  //startdrag
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false; // prevent image block the raycast
    }
    //dragging
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }
    //enddrag
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true; //make image block the raycast
    }
}
