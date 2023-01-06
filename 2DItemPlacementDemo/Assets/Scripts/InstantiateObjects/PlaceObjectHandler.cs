using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceObjectHandler : MonoBehaviour
{
    public static Action<bool> setHasBeenPlacedEvent;
    public static Action<GameObject> setGameObjectEvent;
    public static Action<List<HandleConfirm.ItemsData>> setItemRemoveEvent;
    private List<HandleConfirm.ItemsData> itemData;

    private GameObject currentSelectedObject;
    private GameObject panel;
    private GameObject itemSpawner;

    private bool hasBeenPlaced = false;
    private int counter = 0;

    private void OnEnable()
    {
        InstantiateObjects.getDataEvent += GetData;
        InstantiateObjects.getGameObjectEvent += GetGameObject;
        InstantiateObjects.handleMovementEvent += MoveInstanceUpdate;
    }

    private void OnDisable()
    {
        InstantiateObjects.getDataEvent -= GetData;
        InstantiateObjects.handleMovementEvent -= MoveInstanceUpdate;
        InstantiateObjects.getGameObjectEvent -= GetGameObject;
    }

    private void Start()
    {
        panel = GameObject.Find("Canvas/ShopPanel");
        itemSpawner = GameObject.Find("ItemSpawner");
    }

    private void Update()
    {
        if (itemData.Count > 0)
        {
            panel.SetActive(false);
        }
        else
        {

            panel.SetActive(true);
            itemSpawner.SetActive(false);
        }
    }

    private void MoveInstanceUpdate()
    {
        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame == true)
        {
            if (currentSelectedObject.GetComponent<CollisionWithGroundHandler>().IsObjectTouchingGround)
            {
                if (itemData[0].name == "Desk" || itemData[0].name == "Metal Desk")
                {
                    counter = counter + 1;
                    if (itemData.Count > 0)
                    {
                        PlaceObject(itemData[0].name, itemData[0].quantity, itemData);
                    }
                }
            }
        }
    }

    void PlaceObject(string name, int quantity, List<HandleConfirm.ItemsData> itemData)
    {
        SetHasBeenPlaced(true);
        SetGameObject(null);

        if (counter >= quantity)
        {
            counter = 0;
            itemData.Remove(itemData[0]);
            SetItemData(itemData);
        }
    }

    public void SetHasBeenPlaced(bool _hasBeenPlaced) { setHasBeenPlacedEvent?.Invoke(_hasBeenPlaced); }
    public void SetGameObject(GameObject _currentSelectedObject) { setGameObjectEvent?.Invoke(_currentSelectedObject); }
    public void SetItemData(List<HandleConfirm.ItemsData> _itemData) { setItemRemoveEvent?.Invoke(_itemData); }

    private void GetGameObject(GameObject _currentSelectedObject) { currentSelectedObject = _currentSelectedObject; }
    private void GetData(List<HandleConfirm.ItemsData> purchasedList) { itemData = purchasedList; }
}
