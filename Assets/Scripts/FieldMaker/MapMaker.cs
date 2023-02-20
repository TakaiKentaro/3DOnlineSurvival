using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [SerializeField] private int _fieldSize = 0;

    [SerializeField] private GameObject _fieldGenerator;

    private GameObject[,] _fieldArray;
    private void Start()
    {
        _fieldArray = new GameObject[_fieldSize,_fieldSize];
        MapMake();
    }

    private void Update()
    {
        
    }

    void MapMake()
    {
        int x = 0;
        int z = 0;
        
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                var field = Instantiate(_fieldGenerator);
                _fieldArray[i, j] = field;
                _fieldArray[i,j].transform.position = new Vector3(i * 50, 0, j * 50);
            }
        }
    }
}
