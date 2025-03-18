using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    public GameObject validDropZone;
    public int assignedCardIndex; // �󶨿���
    public CardManager cardManager; // �� CardManager ʵ��

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
                cardManager.PlayCard(cardIndex); // **���� PlayCard**
            }*/
            cardManager.PlayCard(assignedCardIndex);
            }
        //}
       
        
            transform.position = originalPosition; // ��λ
        
    }
    private bool IsValidDropZone(PointerEventData eventData)
    {
        if (validDropZone == null) return false;

        RectTransform dropZoneRect = validDropZone.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(dropZoneRect, Input.mousePosition);
    }
}

