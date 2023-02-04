using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ToDo クラフトレシピの格納を拡張しやすいように修正
/// </summary>
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
        
        // Stick
        Item.ItemType[,] recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 4] = Item.ItemType.None;  recipe[1, 4] = Item.ItemType.None;  recipe[2, 4] = Item.ItemType.None;  recipe[3, 4] = Item.ItemType.None;  recipe[4, 4] = Item.ItemType.None;
        recipe[0, 3] = Item.ItemType.None;  recipe[1, 3] = Item.ItemType.None;  recipe[2, 3] = Item.ItemType.None;  recipe[3, 3] = Item.ItemType.None;  recipe[4, 3] = Item.ItemType.None;
        recipe[0, 2] = Item.ItemType.None;  recipe[1, 2] = Item.ItemType.None;  recipe[2, 2] = Item.ItemType.Wood;  recipe[3, 2] = Item.ItemType.None;  recipe[4, 2] = Item.ItemType.None;
        recipe[0, 1] = Item.ItemType.None;  recipe[1, 1] = Item.ItemType.None;  recipe[2, 1] = Item.ItemType.Wood;  recipe[3, 1] = Item.ItemType.None;  recipe[4, 1] = Item.ItemType.None;
        recipe[0, 0] = Item.ItemType.None;  recipe[1, 0] = Item.ItemType.None;  recipe[2, 0] = Item.ItemType.Wood;  recipe[3, 0] = Item.ItemType.None;  recipe[4, 0] = Item.ItemType.None;
        _recipeDictionary[Item.ItemType.Stick] = recipe;
        
        // SWORD_STONE
        recipe = new Item.ItemType[GRID_SIZE, GRID_SIZE];
        recipe[0, 4] = Item.ItemType.None;  recipe[1, 4] = Item.ItemType.None;  recipe[2, 4] = Item.ItemType.Stone;  recipe[3, 4] = Item.ItemType.None;  recipe[4, 4] = Item.ItemType.None;
        recipe[0, 3] = Item.ItemType.None;  recipe[1, 3] = Item.ItemType.None;  recipe[2, 3] = Item.ItemType.Stone;  recipe[3, 3] = Item.ItemType.None;  recipe[4, 3] = Item.ItemType.None;
        recipe[0, 2] = Item.ItemType.None;  recipe[1, 2] = Item.ItemType.None;  recipe[2, 2] = Item.ItemType.Stone;  recipe[3, 2] = Item.ItemType.None;  recipe[4, 2] = Item.ItemType.None;
        recipe[0, 1] = Item.ItemType.None;  recipe[1, 1] = Item.ItemType.Wood;  recipe[2, 1] = Item.ItemType.Stick;  recipe[3, 1] = Item.ItemType.Wood;  recipe[4, 1] = Item.ItemType.None;
        recipe[0, 0] = Item.ItemType.None;  recipe[1, 0] = Item.ItemType.None;  recipe[2, 0] = Item.ItemType.Stick;  recipe[3, 0] = Item.ItemType.None;  recipe[4, 0] = Item.ItemType.None;
        _recipeDictionary[Item.ItemType.Sword_Stone] = recipe;

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