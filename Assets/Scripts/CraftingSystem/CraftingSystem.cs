using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : IItemHolder
{
    public const int GRID_SIZE = 5;

    private CraftingRecipeData _craftingRecipeData;
    public event EventHandler OnGridChange;

    public Dictionary<Item.ItemType, Item.ItemType[,]> _recipeDictionary;

    private Item[,] _itemArray;
    private Item _outputItem;

    public CraftingSystem()
    {
        _itemArray = new Item[GRID_SIZE, GRID_SIZE];

        _recipeDictionary = new Dictionary<Item.ItemType, Item.ItemType[,]>();
    }

    public bool IsEmpty(int x, int y)
    {
        return _itemArray[x, y] == null;
    }

    public Item GetItem(int x, int y)
    {
        return _itemArray[x, y];
    }

    public void SetItem(Item item, int x, int y)
    {
        if (item != null)
        {
            item.RemoveFromItemHolder();
            item.SetItemHolder(this);
        }

        _itemArray[x, y] = item;
        CreateOutput();
        OnGridChange?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseItemAmount(int x, int y)
    {
        GetItem(x, y).amount++;
        OnGridChange?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseItemAmount(int x, int y)
    {
        if (GetItem(x, y) != null)
        {
            GetItem(x, y).amount--;
            if (GetItem(x, y).amount == 0)
            {
                RemoveItem(x, y);
            }

            OnGridChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RemoveItem(int x, int y)
    {
        SetItem(null, x, y);
    }

    public bool TryAddItem(Item item, int x, int y)
    {
        if (IsEmpty(x, y))
        {
            SetItem(item, x, y);
            return true;
        }
        else
        {
            if (item.itemType == GetItem(x, y).itemType)
            {
                IncreaseItemAmount(x, y);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        if (item == _outputItem)
        {
            ConsumeRecipeItems();
            CreateOutput();
            OnGridChange?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    if (GetItem(x, y) == item)
                    {
                        RemoveItem(x, y);
                    }
                }
            }
        }
    }

    public void AddItem(Item item)
    {
    }

    public bool CanAddItem()
    {
        return false;
    }

    private Item.ItemType GetRecipeOutput()
    {
        foreach (Item.ItemType recipeItemType in _recipeDictionary.Keys)
        {
            Item.ItemType[,] recipe = _recipeDictionary[recipeItemType];

            bool completeRecipe = true;
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    if (recipe[x, y] != Item.ItemType.None)
                    {
                        if (IsEmpty(x, y) || GetItem(x, y).itemType != recipe[x, y])
                        {
                            completeRecipe = false;
                        }
                    }
                }
            }

            if (completeRecipe)
            {
                return recipeItemType;
            }
        }

        return Item.ItemType.None;
    }

    private void CreateOutput()
    {
        Item.ItemType recipeOutput = GetRecipeOutput();
        if (recipeOutput == Item.ItemType.None)
        {
            _outputItem = null;
        }
        else
        {
            _outputItem = new Item { itemType = recipeOutput };
            _outputItem.SetItemHolder(this);
        }
    }

    public Item GetOutputItem()
    {
        return _outputItem;
    }

    public void ConsumeRecipeItems()
    {
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                DecreaseItemAmount(x, y);
            }
        }
    }
}