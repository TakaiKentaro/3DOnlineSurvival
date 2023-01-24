using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

/// <summary>
/// ランダムにマップを生成するクラス
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [Header("シード値")]
    [SerializeField, Tooltip("シード値X")]
    private float _seedX;
    [SerializeField, Tooltip("シード値Z")]
    private float _seedZ;

    [Header("マップサイズ")]
    [SerializeField, Tooltip("幅")]
    private float _width = 50f;
    [SerializeField, Tooltip("深さ")]
    private float _depth = 50f;
    [SerializeField, Tooltip("高さの最大値")]
    private float _maxHeight = 10f;
    [SerializeField, Tooltip("起伏の激しさ")]
    private float _undulation = 15f;
    [SerializeField, Tooltip("マップの大きさ")]
    private float _mapSize = 1f;
    [SerializeField, Tooltip("パーリンノイズを使うか")]
    private bool _isPerlinNoiseMap = true;
    [SerializeField, Tooltip("Y軸の滑らかにするか")]
    private bool _isSmoothness = false;

    private GameObject[,] _fieldArray;
    private void Awake()
    {
        _fieldArray = new GameObject[(int)_width, (int)_depth];
        
        transform.localScale = new Vector3(_mapSize, _mapSize, _mapSize);

        _seedX = Random.value * 100f;
        _seedZ = Random.value * 100f;

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localPosition = new Vector3(x, 0, z);
                cube.transform.localScale = new Vector3(1, 5, 1);
                cube.transform.SetParent(transform);
                _fieldArray[x, z] = cube;
                
                //高さ設定
                SetYPosition(cube);
            }
        }
    }
    private void SetYPosition(GameObject cube)
    {
        float y = 0;

        if (_isPerlinNoiseMap) // パーリンノイズを使って高さを決める
        {
            float xSample = (cube.transform.localPosition.x + _seedX) / _undulation;
            float zSample = (cube.transform.localPosition.z + _seedZ) / _undulation;

            var perlin = new MathfPerlin();

            float noise = perlin.Noise(xSample, zSample);
            
            y = _maxHeight * noise;
        }
        else // ランダムで決める
        {
            y = Random.Range(0, _maxHeight);
        }

        if (!_isSmoothness) // 滑らかにしない場合はyを四捨五入する
        {
            y = Mathf.Round(y);
        }

        cube.transform.localPosition = new Vector3(cube.transform.localPosition.x, y, cube.transform.localPosition.z);

        ChangeCubeColor(cube, y);
    }

    void ChangeCubeColor(GameObject cube, float y) //高さによって色を段階的に変更
    {
        Color color = Color.black; //岩盤

        if (y > _maxHeight * 0.3f) //草
        {
            ColorUtility.TryParseHtmlString("#00ff00", out color);
        }
        else if (y > _maxHeight * 0.2f) // 土
        {
            ColorUtility.TryParseHtmlString("#8b4513", out color);
        }
        else if (y > _maxHeight * 0.1f) // 石
        {
            ColorUtility.TryParseHtmlString("#808080", out color);
        }

        cube.GetComponent<MeshRenderer>().material.color = color;
    }
}
