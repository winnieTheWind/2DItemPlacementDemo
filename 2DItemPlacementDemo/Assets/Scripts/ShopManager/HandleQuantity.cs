using UnityEngine;
using TMPro;

public class HandleQuantity : MonoBehaviour
{
    private ShopItemSO[] furnitureShopItemsSO;

    private void OnEnable() {
        HandleItems.passFurnitureShopItemsEvent += GetFurnitureItems;
        ItemButtonHandler.HandleItemQuantity += SetItemQuantity;
    }

    private void OnDisable() {
        HandleItems.passFurnitureShopItemsEvent -= GetFurnitureItems;
        ItemButtonHandler.HandleItemQuantity -= SetItemQuantity;
    }

    private void GetFurnitureItems(ShopItemSO[] _furnitureShopItemsSO) 
    {
        furnitureShopItemsSO = _furnitureShopItemsSO;
    }

    private void SetItemQuantity(GameObject operatorObject)
    {
        string columnSelected = operatorObject.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text;
        SetOperator(operatorObject, columnSelected, furnitureShopItemsSO);
    }

    private void SetOperator(GameObject operatorObject, string columnSelected, ShopItemSO[] typeShopItemsSO)
    {
        if (operatorObject.GetComponentInChildren<TextMeshProUGUI>().text == "+")
        {
            IncrementQuantity(columnSelected, typeShopItemsSO);
        }
        else if (operatorObject.GetComponentInChildren<TextMeshProUGUI>().text == "-")
        {
            DecrementQuantity(columnSelected, typeShopItemsSO);
        }
    }

    private static void DecrementQuantity(string columnSelected, ShopItemSO[] typeShopItemsSO)
    {
        for (int i = 0; i < typeShopItemsSO.Length; i++)
        {
            if (typeShopItemsSO[i].name == columnSelected)
            {
                if (typeShopItemsSO[i].quantityToBuy == 0)
                {
                    typeShopItemsSO[i].quantityToBuy = 0;
                }
                else
                {
                    typeShopItemsSO[i].quantityToBuy = typeShopItemsSO[i].quantityToBuy - 1;
                }
            }
        }
    }

    private static void IncrementQuantity(string columnSelected, ShopItemSO[] typeShopItemsSO)
    {
        for (int i = 0; i < typeShopItemsSO.Length; i++)
        {
            if (typeShopItemsSO[i].name == columnSelected)
            {
                typeShopItemsSO[i].quantityToBuy = typeShopItemsSO[i].quantityToBuy + 1;
            }
        }
    }
}
