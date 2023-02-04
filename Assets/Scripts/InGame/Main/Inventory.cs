using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Inventory : MonoBehaviour , IItemHolder
{
    public event EventHandler OnItemListChanged;

    private List<Item> _itemsList;
    private Action<Item> _useItemAction;
    public InventorySlot[] _inventorySlotArray;

    public Inventory(Action<Item> useItemAction, int intventorySlotCount)
    {
        this._useItemAction = useItemAction;
        _itemsList = new List<Item>();

        _inventorySlotArray = new InventorySlot[intventorySlotCount];
        for (int i = 0; i < intventorySlotCount; i++)
        {
            _inventorySlotArray[i] = new InventorySlot(i);
        }

        // デバック用
        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 10 });
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 5 });
    }

    public InventorySlot GetEmptyInventorySlot()
    {
        foreach (InventorySlot inventorySlot in _inventorySlotArray)
        {
            if (inventorySlot.IsEmpty())
            {
                return inventorySlot;
            }
        }

        Debug.LogError("空のInventorySlotが見つかりません");
        return null;
    }

    public InventorySlot GetInventorySlotWithItem(Item item)
    {
        foreach (InventorySlot inventorySlot in _inventorySlotArray)
        {
            if (inventorySlot.GetItem() == item)
            {
                return inventorySlot;
            }
        }

        Debug.LogError($"アイテム{item}がInventorySlotで見つかりません");
        return null;
    }

    public void AddItem(Item item)
    {
        _itemsList.Add(item);
        item.SetItemHolder(this);
        GetEmptyInventorySlot().SetItem(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddItemMergeAmount(Item item)
    {
        if (item.IsStackble())
        {
            bool itemAlreadyInInvantory = false;
            foreach (Item inventoryItem in _itemsList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInvantory = true;
                }
            }

            if (!itemAlreadyInInvantory)
            {
                _itemsList.Add(item);
                item.SetItemHolder(this);
                GetEmptyInventorySlot().SetItem(item);
            }
        }
        else
        {
            _itemsList.Add(item);
            item.SetItemHolder(this);
            GetEmptyInventorySlot().SetItem(item);
        }
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        GetInventorySlotWithItem(item).RemoveItem();
        _itemsList.Remove(item);
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public void RemoveItemAmount(Item.ItemType itemType, int amount)
    {
        RemoveItemRemoveAmount(new Item{itemType = itemType,amount = amount});
    }

    public void RemoveItemRemoveAmount(Item item)
    {
        if (item.IsStackble())
        {
            Item itemInventory = null;
            foreach (Item inventoryItem in _itemsList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInventory = inventoryItem;
                }
            }

            if (itemInventory != null && itemInventory.amount <= 0)
            {
                GetInventorySlotWithItem(itemInventory).RemoveItem();
                _itemsList.Remove(itemInventory);
            }
        }
        else
        {
            GetInventorySlotWithItem(item).RemoveItem();
            _itemsList.Remove(item);
        }
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public void AddItem(Item item, InventorySlot inventorySlot)
    {
        _itemsList.Add(item);
        item.SetItemHolder(this);
        inventorySlot.SetItem(item);
        
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        _useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return _itemsList;
    }

    public InventorySlot[] GetInventorySlotArray()
    {
        return _inventorySlotArray;
    }

    public bool CanAddItem()
    {
        return GetEmptyInventorySlot() != null;
    }

    /// <summary>
    /// １つのインベントリスロットを表す
    /// </summary>
    public class InventorySlot
    {
        private int _index;
        private Item _item;

        public InventorySlot(int index)
        {
            this._index = index;
        }

        public Item GetItem()
        {
            return _item;
        }

        public void SetItem(Item item)
        {
            this._item = item;
        }

        public void RemoveItem()
        {
            _item = null;
        }

        public bool IsEmpty()
        {
            return _item == null;
        }
    }
}