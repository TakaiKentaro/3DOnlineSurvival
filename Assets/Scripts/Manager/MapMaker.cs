using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����_���Ƀ}�b�v�𐶐�����N���X
/// </summary>
public class MapMaker : MonoBehaviour
{
    [Header("�V�[�h�l")]
    [SerializeField, Tooltip("�V�[�h�lX")]
    private float _seedX;
    [SerializeField, Tooltip("�V�[�h�lZ")]
    private float _seedZ;

    [Header("�T�C�Y")]
    [SerializeField, Tooltip("��")]
    private float _width = 50f;
    [SerializeField, Tooltip("�[��")]
    private float _depth = 50f;
    [SerializeField, Tooltip("�����̍ő�l")]
    private float _maxHeight = 10f;
    [SerializeField, Tooltip("�N���̌�����")]
    private float _undulation = 15f;
    [SerializeField, Tooltip("�}�b�v�̑傫��")]
    private float _mapSize = 1f;
    [SerializeField, Tooltip("�p�[�����m�C�Y���g����")]
    private bool _isPerlinNoiseMap = true;
    [SerializeField, Tooltip("Y���̊��炩�ɂ��邩")]
    private bool _isSmoothness = false;
    [SerializeField, Tooltip("�R���C�_�[����")]
    private bool _isCollider = false;

    private void Awake()
    {
        transform.localScale = new Vector3(_mapSize, _mapSize, _mapSize);

        _seedX = Random.value * 100f;
        _seedZ = Random.value * 100f;

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localPosition = new Vector3(x, 0, z);
                cube.transform.SetParent(transform);

                if (!_isCollider)
                {
                    Destroy(cube.GetComponent<BoxCollider>());
                }

                //�����ݒ�
                SetYPosition(cube);
            }
        }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying) { return; } // ���s���łȂ���΃X���[

        transform.localScale = new Vector3(_mapSize, _mapSize, _mapSize);

        foreach (Transform child in transform) // �e�L���[�u�̍��WY��ύX
        {
            SetYPosition(child.gameObject);
        }
    }

    private void SetYPosition(GameObject cube)
    {
        float y = 0;

        if (_isPerlinNoiseMap) // �p�[�����m�C�Y���g���č��������߂�
        {
            float xSample = (cube.transform.localPosition.x + _seedX) / _undulation;
            float zSample = (cube.transform.localPosition.z + _seedZ) / _undulation;

            float noise = Mathf.PerlinNoise(xSample, zSample);

            y = _maxHeight * noise;
        }
        else // �����_���Ō��߂�
        {
            y = Random.Range(0, _maxHeight);
        }

        if (!_isSmoothness) // ���炩�ɂ��Ȃ��ꍇ��y���l�̌ܓ�����
        {
            y = Mathf.Round(y);
        }

        cube.transform.localPosition = new Vector3(cube.transform.localPosition.x, y, cube.transform.localPosition.z);

        ChangeCubeColor(cube, y);
    }

    void ChangeCubeColor(GameObject cube, float y) //�����ɂ���ĐF��i�K�I�ɕύX
    {
        Color color = Color.black; //���

        if (y > _maxHeight * 0.3f) //��
        {
            ColorUtility.TryParseHtmlString("#00ff00", out color);
        }
        else if (y > _maxHeight * 0.2f) // �y
        {
            ColorUtility.TryParseHtmlString("#8b4513", out color);
        }
        else if (y > _maxHeight * 0.1f) // ��
        {
            ColorUtility.TryParseHtmlString("#808080", out color);
        }

        cube.GetComponent<MeshRenderer>().material.color = color;
    }
}
