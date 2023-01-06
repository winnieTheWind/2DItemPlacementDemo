using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;

public class InstantiateObjects : MonoBehaviour
{
    public static List<HandleConfirm.ItemsData> purchaseItemData;
    public static Action<List<HandleConfirm.ItemsData>> getDataEvent;
    public static Action handleMovementEvent;
    public static Action<GameObject> getGameObjectEvent;

    [SerializeField] public GameObject deskToSpawn;
    [SerializeField] public GameObject metalDeskToSpawn;

    public bool hasBeenPlaced = false;
    
    private GameObject currentSelectedObject = null;
    private Tilemap map;

    private bool isCreating = false;

    void Start()
    {
        map = GameObject.Find("Grid/Ground").GetComponent<Tilemap>();
    }

    private void OnEnable()
    {
        HandleConfirm.sendPurchaseItemsListEvent += GetPurchasedItems;
        PlaceObjectHandler.setHasBeenPlacedEvent += SetHasBeenPlaced;
        PlaceObjectHandler.setGameObjectEvent += SetGameObject;
        PlaceObjectHandler.setItemRemoveEvent += SetItemData;
    }

    private void OnDisable()
    {
        HandleConfirm.sendPurchaseItemsListEvent -= GetPurchasedItems;
        PlaceObjectHandler.setHasBeenPlacedEvent -= SetHasBeenPlaced;
        PlaceObjectHandler.setGameObjectEvent -= SetGameObject;
        PlaceObjectHandler.setItemRemoveEvent -= SetItemData;
    }

    private void Update()
    {
        if (isCreating == true)
        {
            StartCreation(purchaseItemData);
        }
    }

    private void StartCreation(List<HandleConfirm.ItemsData> itemData)
    {

        if (itemData.Count != 0)
        {
            InstantiateItem("Desk", deskToSpawn);
            InstantiateItem("Metal Desk", metalDeskToSpawn);
        }
        else
        {
            isCreating = false;
        }

        if (hasBeenPlaced == false)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
            Vector3Int mousePositionToCell = map.WorldToCell(new Vector3(mousePosition.x, mousePosition.y, 0));
            Vector3 mousePositionCellToWorld = map.GetCellCenterWorld(mousePositionToCell);

            if (currentSelectedObject != null)
            {
                getGameObjectEvent?.Invoke(currentSelectedObject);
                currentSelectedObject.transform.position = mousePositionCellToWorld;

                if (Keyboard.current.eKey.wasPressedThisFrame == true)
                {
                    Destroy(currentSelectedObject);
                    isCreating = false;
                }
            }
        }

        handleMovementEvent?.Invoke();

    }

    private void InstantiateItem(string type, GameObject prefabToSpawn)
    {
        if (purchaseItemData[0].name == type)
        {
            if (currentSelectedObject == null)
            {
                currentSelectedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
                hasBeenPlaced = false;

            }
        }
    }

    public void GetPurchasedItems(List<HandleConfirm.ItemsData> purchasedList)
    {
        purchaseItemData = purchasedList;
        if (purchaseItemData.Count != 0)
        {
            isCreating = true;
        }
        getDataEvent?.Invoke(purchaseItemData);
    }

    void SetHasBeenPlaced(bool _hasBeenPlaced) { hasBeenPlaced = _hasBeenPlaced; }
    void SetGameObject(GameObject _currentSelectedObject) { currentSelectedObject = _currentSelectedObject; }
    void SetItemData(List<HandleConfirm.ItemsData> _itemsData) { purchaseItemData = _itemsData; }
}
