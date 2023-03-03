using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using ColorUtility = UnityEngine.ColorUtility;

public class FieldBox : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _plane = new MeshRenderer[5];

    private Camera _camera;
    private MeshRenderer _renderer;

    public bool _isPut = false;

    public void ChangeColor(String c)
    {
        Color color = Color.black; //岩盤

        ColorUtility.TryParseHtmlString(c, out color);

        foreach (var box in _plane)
        {
            box.material.color = color;
        }
    }

    void Update()
    {
        //　カメラのビューポートの限界位置をオフセット位置を含めて計算
        /*var leftScreenPos = Camera.main.WorldToViewportPoint(transform.position + new Vector3(0.8f, 0f, 0f));
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
        }*/
    }
}