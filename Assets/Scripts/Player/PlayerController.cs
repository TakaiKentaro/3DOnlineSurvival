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

        PlayerMove();
    }

    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }

        _rb.velocity = dir.normalized * _playerMoveSpeed + _rb.velocity.y * Vector3.up;

    }
}
