using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FieldBox : MonoBehaviour
{
    private Camera _camera;
    private MeshRenderer _renderer;

    private void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _renderer = this.gameObject.GetComponent<MeshRenderer>();
        _renderer.enabled = false;
    }
    
    void Update()
    {
        //　カメラのビューポートの限界位置をオフセット位置を含めて計算
        var leftScreenPos = Camera.main.WorldToViewportPoint(transform.position + new Vector3(0.8f, 0f, 0f));
        var rightScreenPos = Camera.main.WorldToViewportPoint(transform.position - new Vector3(0.8f, 0f, 0f));
        var topScreenPos = Camera.main.WorldToViewportPoint(transform.position - new Vector3(0f, 0.8f, 0f));
        var bottomScreenPos = Camera.main.WorldToViewportPoint(transform.position + new Vector3(0f, 0.8f, 0f));
        //　ターゲットのゲームオブジェクトが限界位置を越えてなければ計算
        if (0f <= leftScreenPos.x && rightScreenPos.x <= 1f && 0f <= bottomScreenPos.y && topScreenPos.y <= 1f)
        {
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }
    }
}