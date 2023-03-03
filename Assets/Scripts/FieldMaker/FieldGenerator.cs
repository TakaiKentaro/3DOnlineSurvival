using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

/// <summary>
/// ランダムにマップを生成するクラス
/// </summary>
public class FieldGenerator : MonoBehaviour
{
    [Header("シード値")]
    [SerializeField, Tooltip("シード値X")]
    private float _seedX;

    [SerializeField, Tooltip("シード値Z")] private float _seedZ;

    [Header("マップサイズ")]
    [SerializeField, Tooltip("幅")]
    private float _width = 50f;

    [SerializeField, Tooltip("深さ")] private float _depth = 50f;
    [SerializeField, Tooltip("高さの最大値")] private float _maxHeight = 10f;
    [SerializeField, Tooltip("起伏の激しさ")] private float _undulation = 15f;
    [SerializeField, Tooltip("マップの大きさ")] private float _mapSize = 1f;

    [SerializeField, Tooltip("パーリンノイズを使うか")]
    private bool _isPerlinNoiseMap = true;

    [SerializeField, Tooltip("Y軸の滑らかにするか")]
    private bool _isSmoothness = false;

    [SerializeField, Tooltip("Field用Box")] 
    private GameObject _fieldBox;

    [SerializeField, Tooltip("Field用Tree")]
    private GameObject _fieldTree;

    [SerializeField, Tooltip("Field用Stone")]
    private GameObject _fieldStone;

    private FieldBox[,] _fieldArray;

    private void Awake()
    {
        _fieldArray = new FieldBox[(int)_width, (int)_depth];

        transform.localScale = new Vector3(_mapSize, _mapSize, _mapSize);

        _seedX = Random.value * 100f;
        _seedZ = Random.value * 100f;

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                GameObject cube = Instantiate(_fieldBox);
                cube.transform.localPosition = new Vector3(x, 0, z);
                cube.transform.SetParent(transform);
                _fieldArray[x, z] = cube.GetComponent<FieldBox>();

                //高さ設定
                SetYPosition(cube);
            }
        }

        //PutObject();
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

        // ChangeCubeColor(cube, y);
    }

    void ChangeCubeColor(GameObject cube, float y) //高さによって色を段階的に変更
    {
        if (y > _maxHeight * 0.3f) //草
        {
            cube.GetComponent<FieldBox>().ChangeColor("#00ff00");
        }
        else if (y > _maxHeight * 0.2f) // 土
        {
            cube.GetComponent<FieldBox>().ChangeColor("#8b4513");
        }
        else if (y > _maxHeight * 0.1f) // 石
        {
            cube.GetComponent<FieldBox>().ChangeColor("#808080");
        }
        else
        {
            cube.GetComponent<FieldBox>().ChangeColor("#000000");
        }
    }

    void PutObject()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                FieldBox go = _fieldArray[x, z];

                if (PerimeterCheck(x, z))
                {
                    if (go.transform.position.y > _maxHeight * 0.3f)
                    {
                        Instantiate(_fieldTree, new Vector3(0, 5f, 0), Quaternion.identity);
                        go._isPut = true;
                    }
                    else if (go.transform.position.y > _maxHeight * 0.1f)
                    {
                        Instantiate(_fieldStone, new Vector3(0, 5f, 0), Quaternion.identity);
                        go._isPut = true;
                    }
                }
            }
        }
    }

    bool PerimeterCheck(int x, int z)
    {
        if (x + 1 < _width && x - 1 >= 0 && z + 1 < _depth && z - 1 >= 0)
        {
            if (!_fieldArray[x + 1, z]._isPut && !_fieldArray[x - 1, z]._isPut
                                              && !_fieldArray[x, z + 1]._isPut && !_fieldArray[x, z - 1]._isPut
                                              && !_fieldArray[x + 1, z + 1]._isPut && !_fieldArray[x - 1, z - 1]._isPut
                                              && !_fieldArray[x + 1, z - 1]._isPut && !_fieldArray[x - 1, z + 1]._isPut)
            {
                _fieldArray[x + 1, z]._isPut = true;
                _fieldArray[x - 1, z]._isPut = true;
                _fieldArray[x, z + 1]._isPut = true;
                _fieldArray[x, z - 1]._isPut = true;
                _fieldArray[x + 1, z + 1]._isPut = true;
                _fieldArray[x - 1, z - 1]._isPut = true;
                _fieldArray[x + 1, z - 1]._isPut = true;
                _fieldArray[x - 1, z + 1]._isPut = true;

                return true;
            }

            return false;
        }

        return false;
    }
}