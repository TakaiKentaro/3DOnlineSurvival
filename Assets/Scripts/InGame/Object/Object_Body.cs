using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Body : MonoBehaviour
{
    
    private Color _color;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    public void HPCalculation(int damage)
    {
    }

    private Color SetColor()
    {
        
        return Color.blue;
    }

    private int SetHp()
    {
        return 0;
    }
}