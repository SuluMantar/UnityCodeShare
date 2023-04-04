using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TMP_Text amountText;

    public Transform parentAfterDrag;
    public Item item;
    public int itemAmount = 1;

    private InventoryManager inventory;

    public void InitialiseItem(Item newItem, int quantity)
    {
        item = newItem;
        image.sprite = newItem.image;
        itemAmount = quantity;
        RefreshCount();
        Debug.Log(item.name);
    }

    public void RefreshCount()
    {
        amountText.text = itemAmount.ToString();
        bool textActive = itemAmount > 1;
        amountText.gameObject.SetActive(textActive);

    }

    public void OnBeginDrag(PointerEventData eventdata){

        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }


    public void OnDrag(PointerEventData eventdata){

        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventdata){

        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    public int GetQuantity()
    {
        return itemAmount;

    }



}
