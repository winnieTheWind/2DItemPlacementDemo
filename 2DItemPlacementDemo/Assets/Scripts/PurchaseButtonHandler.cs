using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class PurchaseButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public static event Action PurchaseEvent;
    public static event Action HireEvent;
    private string typeButton;

    public GameObject itemSpawner;

    private ShopItemSO[] furnitureShopItemsSO;
    private ShopItemSO[] equipmentShopItemsSO;

    private void OnEnable()
    {
        HandleItems.passFurnitureShopItemsEvent += GetFurnitureItems;
    }

    void GetFurnitureItems(ShopItemSO[] furnitureItems)
    {
        furnitureShopItemsSO = furnitureItems;

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        itemSpawner.SetActive(true);
        PurchaseEvent?.Invoke();
    }
}
