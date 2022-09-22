using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionCotroller : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] GameObject _attackCollider;
    [SerializeField] GameObject _attacFakeCollider;

    [Header("Guard")]
    [SerializeField] GameObject _guardCollider;

    [Header("Status")]
    [SerializeField] float _intervalTime;
    bool _check;

    private void Start()
    {
        _attacFakeCollider.SetActive(true);
        _attackCollider.SetActive(false);
        _guardCollider.SetActive(false);
    }

    private void Update()
    {
        if (!_check)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _attacFakeCollider.SetActive(false);
                _attackCollider.SetActive(true);
                _check = true;
                StartCoroutine("ActionInterval");
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            _guardCollider.SetActive(true);
            _check = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            _guardCollider.SetActive(false);
            _check = false;
        }
    }

    IEnumerator ActionInterval()
    {
        yield return new WaitForSeconds(_intervalTime);
        _check = false;
        _attacFakeCollider.SetActive(true);
        _attackCollider.SetActive(false);
    }
}
