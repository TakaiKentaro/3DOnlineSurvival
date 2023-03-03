using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [SerializeField] private int _fieldSize = 0;
    [SerializeField] private GameObject _fieldGenerator;
    [SerializeField] private MeshCombiner _meshConbiner;

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
        for (int x = 0; x < _fieldSize; x++)
        {
            for (int z = 0; z < _fieldSize; z++)
            {
                var field = Instantiate(_fieldGenerator);
                field.transform.parent = transform;
                _fieldArray[x, z] = field;
                _fieldArray[x,z].transform.position = new Vector3(x * 50, 0, z * 50);
            }
        }

    }
}
