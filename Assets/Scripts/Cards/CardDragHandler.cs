using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    public GameObject validDropZone;
    public int assignedCardIndex; // 绑定卡牌
    public CardManager cardManager; // 绑定 CardManager 实例

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (IsValidDropZone(eventData))
        //{
            Debug.Log("Card dropped in valid zone.");
            if (cardManager != null )
            {

            /*int cardIndex = cardManager.deck.IndexOf(assignedCard);
        Debug.Log(cardManager.deck.IndexOf(assignedCard));*/
            /*if (cardIndex != -1)
            {
                cardManager.PlayCard(cardIndex); // **调用 PlayCard**
            }*/
            cardManager.PlayCard(assignedCardIndex);
            }
        //}
       
        
            transform.position = originalPosition; // 复位
        
    }
    private bool IsValidDropZone(PointerEventData eventData)
    {
        if (validDropZone == null) return false;

        RectTransform dropZoneRect = validDropZone.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(dropZoneRect, Input.mousePosition);
    }
}

