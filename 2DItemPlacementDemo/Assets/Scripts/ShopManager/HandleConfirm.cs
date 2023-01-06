using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandleConfirm : MonoBehaviour
{
    private ShopItemSO[] furnitureShopItemsSO;
    private List<ItemsData> purchaseItemlist = new List<ItemsData>();

    public static Action<List<ItemsData>> sendPurchaseItemsListEvent;

    public struct ItemsData
    {
        public string name;
        public int quantity;
    }

    private void OnEnable()
    {
        PurchaseButtonHandler.PurchaseEvent += PurchaseItems;
        HandleItems.passFurnitureShopItemsEvent += GetFurnitureItems;
    }

    private void OnDisable()
    {
        PurchaseButtonHandler.PurchaseEvent -= PurchaseItems;
        HandleItems.passFurnitureShopItemsEvent -= GetFurnitureItems;
    }

    private void PurchaseItems()
    {
        purchaseItemlist.Clear();
        SetItems(furnitureShopItemsSO);

        sendPurchaseItemsListEvent?.Invoke(purchaseItemlist);
    }

    private void SetItems(ShopItemSO[] typeShopItemsSO)
    {
        for (int i = 0; i < typeShopItemsSO.Length; i++)
        {
            if (typeShopItemsSO[i].quantityToBuy > 0)
            {
                ItemsData item = new ItemsData();
                item.name = typeShopItemsSO[i].name;
                item.quantity = typeShopItemsSO[i].quantityToBuy;
                purchaseItemlist.Add(item);
            }
        }
    }

    private void GetFurnitureItems(ShopItemSO[] _furnitureShopItemsSO)
    {
        furnitureShopItemsSO = _furnitureShopItemsSO;
    }
}
