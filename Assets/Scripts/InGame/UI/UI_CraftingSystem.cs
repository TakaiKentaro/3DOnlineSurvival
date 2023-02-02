using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class UI_CraftingSystem : MonoBehaviour
{
    [SerializeField] private Transform pfUI_Item;

    private Transform[,] _slotTransformArray;
    private Transform _outputSlotTransform;
    private Transform _itemContainer;
    private CraftingSystem _craftingSystem;

    private void Awake()
    {
        _itemContainer = transform.Find("ItemContainer");
        Transform gridContainer = transform.Find("GridContainer");

        _slotTransformArray = new Transform[CraftingSystem.GRID_SIZE, CraftingSystem.GRID_SIZE];
    }
}