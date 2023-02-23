using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] float _movingSpeed = 0;
    [SerializeField] float _turnSpeed = 0;
    [SerializeField] float _jumpPower = 0;
    [SerializeField] float _isGroundedLength = 0;
    [SerializeField] Animator _anim;

    Rigidbody _rb;
    PhotonView _view;

    void Start()
    {
        _view = GetComponent<PhotonView>();
        if (_view)
        {
            if (_view.IsMine)
            {
                _rb = GetComponent<Rigidbody>();
                _anim = GetComponent<Animator>();
            }
        }
    }

    void Update()
    {
        if (!_view.IsMine) return;
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);

            Vector3 velo = dir.normalized * _movingSpeed;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;

        }
        if (_anim)
        {
            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _anim.SetFloat("Speed", velocity.magnitude);

            if (_rb.velocity.y <= 0f && IsGrounded())
            {
                _anim.SetBool("IsGrounded", true);
            }
            else if (!IsGrounded())
            {
                _anim.SetBool("IsGrounded", false);
            }
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Debug.Log("Jump");
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

            if (_anim)
            {
                _anim.SetBool("IsGrounded", false);
            }
        }
    }

    bool IsGrounded()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;   
        Vector3 end = start + Vector3.down * _isGroundedLength;  
        Debug.DrawLine(start, end); 
        bool isGrounded = Physics.Linecast(start, end); 
        return isGrounded;
    }
}