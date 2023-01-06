using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ItemButtonHandler : MonoBehaviour, IPointerDownHandler
{
    public static Action<GameObject> HandleItemQuantity;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        HandleItemQuantity?.Invoke(gameObject);
    }
}