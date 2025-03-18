using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    public GameObject validDropZone;
    public Card assignedCard; // �󶨿���
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
        if (IsValidDropZone(eventData))
        {
            Debug.Log("Card dropped in valid zone.");
            //cardManager.PlayCard(deck.IndexOf(assignedCard)); // ���� PlayCard
        }
        else
        {
            transform.position = originalPosition; // ��λ
        }
    }
    private bool IsValidDropZone(PointerEventData eventData)
    {
        if (validDropZone == null) return false;

        RectTransform dropZoneRect = validDropZone.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(dropZoneRect, Input.mousePosition);
    }
}

