using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    None,
    
    Wood,

    Stone,
}


public class Object_Root : MonoBehaviour
{
    [SerializeField] private ObjectType _objectType;
    [SerializeField] private int _hp;
    
    [SerializeField] private Object_Body _body;
    [SerializeField] private Object_UI _ui;

    private void Start()
    {
        _body.SetObject(_objectType);
        _ui.SetObject(_objectType,_hp);
    }
}
