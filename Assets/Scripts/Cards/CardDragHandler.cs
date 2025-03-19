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
    private Vector2 targetpos;
    public Transform fireTransform;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //问题定位
        transform.position = Input.mousePosition;
        Debug.Log(transform.position);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
            Debug.Log("Card dropped in valid zone.");
            if (cardManager != null )
            {
                targetpos = Camera_Ray(transform.position);
                cardManager.PlayCard(assignedCardIndex,targetpos,fireTransform);
            }
            transform.position = originalPosition; // 复位
    }
    private Vector3 Camera_Ray(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        //if(isCollider != null)
        return hit.point;
    }
    private bool IsValidDropZone(PointerEventData eventData)
    {
        if (validDropZone == null) return false;

        RectTransform dropZoneRect = validDropZone.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(dropZoneRect, Input.mousePosition);
    }
}

