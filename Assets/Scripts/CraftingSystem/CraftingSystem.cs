using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : IItemHolder
{
    public const int GRID_SIZE = 5;

    public event EventHandler OnGridChange;

    private Dictionary<Item.ItemType, Item.ItemType[,]> _recipeDictionary;

    private Item[,] _itemArray;
    private Item _outputItem;


    public bool IsEmpty(int x,int y)
    {
        return _itemArray[x, y] == null;
    }

    public Item GetItem(int x, int y)
    {
        return _itemArray[x, y];
    }

    public void SetItem(Item item, int x, int y)
    {
        if (!object.ReferenceEquals(item, null))
        {
            item.RemoveFromItemHolder();
            item.SetItemHolder(this);
        }
        _itemArray[x, y] = item;
        //CreateOutput();
        OnGridChange?.Invoke(this,EventArgs.Empty);
    }

    public void IncreaseItemAmount(int x, int y)
    {
        GetItem(x, y).amount++;
        OnGridChange?.Invoke(this,EventArgs.Empty);
    }

    public void DecreaseItemAmount(int x, int y)
    {
        if (!object.ReferenceEquals(GetItem(x, y), null))
        {
            GetItem(x, y).amount--;
            if (object.ReferenceEquals(GetItem(x, y), 0))
            {
                RemoveItem(x, y);
            }
            OnGridChange?.Invoke(this,EventArgs.Empty);
        }
    }

    public void RemoveItem(int x, int y)
    {
        SetItem(null,x,y);
    }

    public bool TryAddItem(Item item, int x, int y)
    {
        if (IsEmpty(x, y))
        {
            SetItem(item,x,y);
            return true;
        }
        else
        {
            if (object.ReferenceEquals(item.itemType, GetItem(x, y).itemType))
            {
                IncreaseItemAmount(x,y);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    






}
