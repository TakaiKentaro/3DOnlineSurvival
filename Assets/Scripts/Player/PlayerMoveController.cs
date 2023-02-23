using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
public class PlayerMoveController : MonoBehaviour
{
    /// <summary>��������</summary>
    [SerializeField] float _movingSpeed = 5f;
    /// <summary>�^�[���̑���</summary>
    [SerializeField] float _turnSpeed = 3f;
    /// <summary>�W�����v��</summary>
    [SerializeField] float _jumpPower = 5f;
    /// <summary>�ڒn����̍ہA���S (Pivot) ����ǂꂭ�炢�̋������u�ڒn���Ă���v�Ɣ��肷�邩�̒���</summary>
    [SerializeField] float _isGroundedLength = 1.1f;
    /// <summary>�L�����N�^�[�� Animator</summary>
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
                // �������i�����ő��삵�ē������j�I�u�W�F�N�g�̏ꍇ�̂� Rigidbody, Animator ���g��
                _rb = GetComponent<Rigidbody>();
                _anim = GetComponent<Animator>();
            }
        }
    }

    void Update()
    {
        if (!_view.IsMine) return;
        // �����̓��͂��擾���A���������߂�
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // �����̓��͂��j���[�g�����̎��́Ay �������̑��x��ێ����邾��
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɃL�����N�^�[��������
            dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
            dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���

            // ���͕����Ɋ��炩�ɉ�]������
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);

            Vector3 velo = dir.normalized * _movingSpeed; // ���͂��������Ɉړ�����
            velo.y = _rb.velocity.y;   // �W�����v�������� y �������̑��x��ێ�����
            _rb.velocity = velo;   // �v�Z�������x�x�N�g�����Z�b�g����

        }
        // Animator Controller �̃p�����[�^���Z�b�g����
        if (_anim)
        {
            // ���������̑��x�� Speed �ɃZ�b�g����
            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _anim.SetFloat("Speed", velocity.magnitude);

            // �n��/�󒆂̏󋵂ɉ����� IsGrounded ���Z�b�g����
            if (_rb.velocity.y <= 0f && IsGrounded())
            {
                _anim.SetBool("IsGrounded", true);
            }
            else if (!IsGrounded())
            {
                _anim.SetBool("IsGrounded", false);
            }
        }

        // �W�����v�̓��͂��擾���A�ڒn���Ă��鎞�ɉ�����Ă�����W�����v����
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Debug.Log("Jump");
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

            // Animator Controller �̃p�����[�^���Z�b�g����
            if (_anim)
            {
                _anim.SetBool("IsGrounded", false);
            }
        }
    }

    /// <summary>
    /// �n�ʂɐڐG���Ă��邩���肷��
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        // Physics.Linecast() ���g���đ���������𒣂�A�����ɉ������Փ˂��Ă����� true �Ƃ���
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;   // start: �̂̒��S
        Vector3 end = start + Vector3.down * _isGroundedLength;  // end: start ����^���̒n�_
        Debug.DrawLine(start, end); // ����m�F�p�� Scene �E�B���h�E��Ő���\������
        bool isGrounded = Physics.Linecast(start, end); // ���������C���ɉ������Ԃ����Ă����� true �Ƃ���
        return isGrounded;
    }
}