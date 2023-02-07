using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Transform pfUI_Item;
    [SerializeField] private Transform _itemSlotContainer;
    [SerializeField] private Transform _itemSlotTemplate;

    private Inventory _inventory;
    private TestPlayer _player;

    private void Awake()
    {
        _itemSlotTemplate.gameObject.SetActive(false);
    }

    public void SetPlayer(TestPlayer player)
    {
        this._player = player;
    } 

    public void SetInventory(Inventory inventory)
    {
        this._inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in _itemSlotContainer)
        {
            if (child == _itemSlotTemplate)
            {
                continue;
            }

            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;
        foreach (Inventory.InventorySlot inventorySlot in _inventory.GetInventorySlotArray())
        {
            // Debug.Log(_inventory.GetInventorySlotArray());

            Item item = inventorySlot.GetItem();

            RectTransform itemSlotRectTransform =
                Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //_inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                if (item.IsStackble())
                {
                    if (item.amount > 2)
                    {
                        if (Input.GetKey("shift")) // 量を半分
                        {
                            int splitAmount = Mathf.FloorToInt(item.amount / 2f);
                            item.amount -= splitAmount;
                            Item duplicateItem = new Item { itemType = item.itemType, amount = splitAmount };
                            _inventory.AddItem(duplicateItem);
                        }
                        else // １つのみ
                        {
                            int drawAmount = 1;
                            item.amount -= drawAmount;
                            Item duplicateItem = new Item { itemType = item.itemType, amount = drawAmount };
                            _inventory.AddItem(duplicateItem);
                        }
                    }
                }
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);

            if (!inventorySlot.IsEmpty())
            {
                Transform uiItemTransform = Instantiate(pfUI_Item, _itemSlotContainer);
                uiItemTransform.GetComponent<RectTransform>().anchoredPosition = itemSlotRectTransform.anchoredPosition;
                UI_Item uiItem = uiItemTransform.GetComponent<UI_Item>();
                uiItem.SetItem(item);
            }

            Inventory.InventorySlot tmpInventorySlot = inventorySlot;

            UI_ItemSlot uiItemSlot = itemSlotRectTransform.GetComponent<UI_ItemSlot>();
            uiItemSlot.SetOnDropAction(() =>
            {
                Item draggedItem = UI_ItemDrag.Instance.GetItem();
                draggedItem.RemoveFromItemHolder();
                _inventory.AddItem(draggedItem, tmpInventorySlot);
            });

            x++;
            int itemRowMax = 7;
            if (x >= itemRowMax)
            {
                x = 0;
                y++;
            }
        }
    }
}