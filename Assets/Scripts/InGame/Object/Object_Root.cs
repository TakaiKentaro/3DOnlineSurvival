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
    
    [SerializeField] private Object_Body _body;
    [SerializeField] private Object_UI _ui;
}
