using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler , IPointerEnterHandler
{


    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    [HideInInspector] public DropItem dropItem;
    public GameObject tooltipPanel;  // กำหนดจาก Inspector
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI descriptionText;

    public string itemName;
    public string description;

    void Start()
    {
        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }


    public void InitialiseItem(DropItem newItem)
    {
        Debug.Log(newItem);
        dropItem = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
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
    
      public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Show Tooltip");

        Debug.Log(ItemToolTipUI.instance);
        Debug.Log(dropItem);
        if (ItemToolTipUI.instance != null && dropItem != null)
        {
            ItemToolTipUI.instance.ShowToolTip(dropItem, Input.mousePosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hide Tooltip");
        ItemToolTipUI.instance.HideToolTip();
        
    }
}
