using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [SerializeField] private int _fieldSize = 0;

    [SerializeField] private GameObject _fieldGenerator;

    private void Start()
    {
        MapMake();
    }

    private void Update()
    {
        
    }

    void MapMake()
    {
        int x = 0;
        int z = 0;

        Vector3 pos = new Vector3(x * 100,0,z * 100);
        
        for (int i = 0; i < Math.Pow(_fieldSize,_fieldSize); i++)
        {
            if (x == z)
            {
                Instantiate(_fieldGenerator, pos,Quaternion.identity);
                x++;
            }
            else if (x > z)
            {
                z += 2;
            }
            else if (z > x)
            {
                
            }
        }
    }
}
