using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Object_UI : MonoBehaviour
{
    [Header("TEXT")]
    [SerializeField] private TextMeshProUGUI _text;

    [Header("Slider")] 
    [SerializeField] private Slider _hpSlider;
    
    
    [Header("Camera")]
    [SerializeField] float _hideDistance = 0;
    [SerializeField] Camera _camera;

    private int _hp;
    private ObjectType _type;

    public void SetObject(ObjectType type, int hp)
    {
        _type = type;
        _text.text = _type.ToString();
        _hp = hp;
        _hpSlider.maxValue = hp;
    }

    void OnDamage()
    {
        
    }

    private void Update()
    {
        var camera = Vector3.Distance(_camera.transform.position, transform.position);

        if(camera <= _hideDistance)
        {
            Vector3 vector3 = _camera.transform.position - this.transform.position;
            Quaternion quaternion = Quaternion.LookRotation(vector3);
            this.transform.rotation = quaternion;
        }
    }
}
