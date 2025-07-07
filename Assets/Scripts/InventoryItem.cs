using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler 
{

    [Header("UI")]
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public DropItem dropItem;

    
    public void InitialiseItem(DropItem newItem)
    {
        dropItem =  newItem;
        image.sprite = newItem.image;
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
