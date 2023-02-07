using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class UI_CraftingSystem : MonoBehaviour
{
    [SerializeField] private Transform pfUI_Item;
    [SerializeField] private  Transform _itemContainer;
    [SerializeField] private Transform _gridContainer;
    
    private Transform[,] _slotTransformArray;
    private Transform _outputSlotTransform;
    private CraftingSystem _craftingSystem;

    private void Awake()
    {
        _slotTransformArray = new Transform[CraftingSystem.GRID_SIZE, CraftingSystem.GRID_SIZE];

        for (int x = 0; x < CraftingSystem.GRID_SIZE; x++)
        {
            for (int y = 0; y < CraftingSystem.GRID_SIZE; y++)
            {
                _slotTransformArray[x,y]  = _gridContainer.Find($"Grid_{x}_{y}");
                UI_CraftingItemSlot craftingItemSlot = _slotTransformArray[x, y].GetComponent<UI_CraftingItemSlot>();
                craftingItemSlot.SetXY(x,y);
                craftingItemSlot.OnItemDropped += UI_CraftingSystem_OnItemDropped;
            }
        }

        _outputSlotTransform = transform.Find("OutputSlot");
        
        //CreateItem(0, 0, new Item { itemType = Item.ItemType.Stick });
        //CreateItem(1, 2, new Item { itemType = Item.ItemType.Stone });
        //CreateItemOutput(new Item { itemType = Item.ItemType.Sword_Stone });
    }

    public void SetCraftingSystem(CraftingSystem craftingSystem)
    {
        this._craftingSystem = craftingSystem;
        craftingSystem.OnGridChanged += CraftingSystem_OnGridChanged;

        UpdateVisual();
    }

    private void CraftingSystem_OnGridChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UI_CraftingSystem_OnItemDropped(object sender, UI_CraftingItemSlot.OnItemDroppedEventArgs e)
    {
        _craftingSystem.TryAddItem(e.item, e.x, e.y);
    }

    private void UpdateVisual()
    {
        // 古いアイテムを削除
        foreach (Transform child in _itemContainer)
        {
            Destroy(child.gameObject);
        }
        
        //グリッドの循環とアイテムの生成
        for (int x = 0; x < CraftingSystem.GRID_SIZE; x++)
        {
            for (int y = 0; y < CraftingSystem.GRID_SIZE; y++)
            {
                if (!_craftingSystem.IsEmpty(x, y))
                {
                    CreatItem(x, y, _craftingSystem.GetItem(x, y));
                }
            }
        }

        if (_craftingSystem.GetOutputItem() != null)
        {
            CreatItemOutput(_craftingSystem.GetOutputItem());
        }
    }

    private void CreatItem(int x, int y, Item item)
    {
        Transform itemTransform = Instantiate(pfUI_Item, _itemContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = _slotTransformArray[x, y].GetComponent<RectTransform>().anchoredPosition;
        itemTransform.GetComponent<UI_Item>().SetItem(item);
        Debug.Log(x+","+y+"に"+item.itemType);
    }

    private void CreatItemOutput(Item item)
    {
        Transform itemTransform = Instantiate(pfUI_Item, _itemContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = _outputSlotTransform.GetComponent<RectTransform>().anchoredPosition;
        itemTransform.localScale = Vector3.one * 1.5f;
        itemTransform.GetComponent<UI_Item>().SetItem(item);
    }
}