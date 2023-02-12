using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu( menuName = "ScriptableObject", fileName = "CraftRecipeData" )]
public class CraftingRecipeData : ScriptableObject
{
    public Item.ItemType[,] _itemTypes = new Item.ItemType[5,5];

    public Item.ItemType _recipe = Item.ItemType.None;
}
