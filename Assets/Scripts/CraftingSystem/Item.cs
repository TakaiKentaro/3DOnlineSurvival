using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        None,
        Wood,
        Stick,
        Stone,
        Sword_Stone,
    }

    public ItemType itemType;
    public int amount = 1;
    private IItemHolder itemHolder;

    public void SetItemHolder(IItemHolder itemHolder)
    {
        this.itemHolder = itemHolder;
    }

    public IItemHolder GetItemHolder()
    {
        return itemHolder;
    }

    public void RemoveFromItemHolder()
    {
        if (!object.ReferenceEquals(itemHolder, null))
        {
            itemHolder.RemoveItem(this);
        }
    }

    public void MoveToAnotherItemHolder(IItemHolder newItemHolder)
    {
        RemoveFromItemHolder();
        newItemHolder.AddItem(this);
    }

    public Sprite GetSprite()
    {
        return GetSprite(itemType);
    }
    
    
    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            //素材
            case ItemType.Wood: return ItemAssets.Instance.s_Wood;
            case ItemType.Stick: return ItemAssets.Instance.s_Stick;
            case ItemType.Stone: return ItemAssets.Instance.s_Stone;
            
            //武器
            case ItemType.Sword_Stone: return ItemAssets.Instance.s_Sword_Stone;
        }
    }

    public Color GetColor()
    {
        return GetColor(itemType);
    }

    public static Color GetColor(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Wood: return new Color(1, 1, 1);
            case ItemType.Stick: return new Color(1, 1, 1);
            case ItemType.Stone: return new Color(1, 1, 1);
            case ItemType.Sword_Stone: return new Color(1, 1, 1);
        }
    }

    public bool IsStackble()
    {
        return IsStackble(itemType);
    }

    public static bool IsStackble(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            // 重ね可能   
            case ItemType.Wood:
            case ItemType.Stick:
            case ItemType.Stone:
                return true;

            // 重ね不可能
            case ItemType.Sword_Stone:
                return false;
        }
    }

    public int GetCost()
    {
        return GetCost(itemType);
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
                return 0;
        }
    }

    public override string ToString()
    {
        return itemType.ToString();
    }

    /*public CharacterEquipment.EquipSlot GetEquipSlot()
    {
        switch (itemType)
        {
         
            default:
                return CharacterEquipment.EquipSlot.None;
        
                return CharacterEquipment.EquipSlot.Armor;
        
                return CharacterEquipment.EquipSlot.Helmet;

            case ItemType.Sword_Stone:
                return CharacterEquipment.EquipSlot.Wepon;   
        }
    }*/
}