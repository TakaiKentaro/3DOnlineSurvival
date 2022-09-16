using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _playerMoveSpeed;

    PhotonView _view;
    Rigidbody _rb;

    void Start()
    {
        _view = gameObject.GetPhotonView();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_view.IsMine) { return; }
    }
}
