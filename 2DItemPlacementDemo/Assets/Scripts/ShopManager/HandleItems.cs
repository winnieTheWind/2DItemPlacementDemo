using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HandleItems : MonoBehaviour
{

    public ShopItemSO[] furnitureShopItemsSO;
    public ShopTemplateSO[] furnitureShopPanels;
    public static Action<ShopItemSO[]> passFurnitureShopItemsEvent;

    private bool isSettingItems;

    private void OnEnable()
    {
        SetQuantity(furnitureShopItemsSO, 0);
        passFurnitureShopItemsEvent?.Invoke(furnitureShopItemsSO);
    }

    private void Update()
    {
        SetPanels();
    }

    private void SetPanels()
    {
        for (int i = 0; i < furnitureShopItemsSO.Length; i++)
        {
            furnitureShopPanels[i].tileTxt.text = furnitureShopItemsSO[i].name;
            furnitureShopPanels[i].quantityTxt.text = furnitureShopItemsSO[i].quantityToBuy.ToString();
        }
    }

    private void SetQuantity(ShopItemSO[] shopItemsSO, int quantityNumber)
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopItemsSO[i].quantityToBuy = quantityNumber;
        }
    }
}
