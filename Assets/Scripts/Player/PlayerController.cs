using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{
    /// <summary>����n�̃^�C�v</summary>
    [SerializeField] ControlType _controlType = ControlType.MoveWithSmoothTurn;
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

    Rigidbody m_rb;
    PhotonView m_view;

    void Start()
    {
        m_view = GetComponent<PhotonView>();
        if (m_view)
        {
            if (m_view.IsMine)
            {
                // �������i�����ő��삵�ē������j�I�u�W�F�N�g�̏ꍇ�̂� Rigidbody, Animator ���g��
                m_rb = GetComponent<Rigidbody>();
                _anim = GetComponent<Animator>();
            }
        }
    }

    void Update()
    {
        if (!m_view.IsMine) return;
        // �����̓��͂��擾���A���������߂�
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // ControlType �Ɠ��͂ɉ����ăL�����N�^�[�𓮂���
        if (_controlType == ControlType.Turn)
        {
            // ���E�ŉ�]������
            if (h != 0)
            {
                this.transform.Rotate(this.transform.up, h * _turnSpeed);
            }

            // �㉺�őO��ړ�����B�W�����v�������� y �������̑��x�͕ێ�����B
            Vector3 velo = this.transform.forward * _movingSpeed * v;
            velo.y = m_rb.velocity.y;
            m_rb.velocity = velo;
        }
        else if (_controlType == ControlType.Move || _controlType == ControlType.MoveWithSmoothTurn)
        {
            // ���͕����̃x�N�g����g�ݗ��Ă�
            Vector3 dir = Vector3.forward * v + Vector3.right * h;

            if (dir == Vector3.zero)
            {
                // �����̓��͂��j���[�g�����̎��́Ay �������̑��x��ێ����邾��
                m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            }
            else
            {
                // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɃL�����N�^�[��������
                dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
                dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���

                if (_controlType == ControlType.Move)
                {
                    this.transform.forward = dir;   // ���͂��������ɃI�u�W�F�N�g��������
                }
                else if (_controlType == ControlType.MoveWithSmoothTurn)
                {
                    // ���͕����Ɋ��炩�ɉ�]������
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
                }
                
                Vector3 velo = dir.normalized * _movingSpeed; // ���͂��������Ɉړ�����
                velo.y = m_rb.velocity.y;   // �W�����v�������� y �������̑��x��ێ�����
                m_rb.velocity = velo;   // �v�Z�������x�x�N�g�����Z�b�g����
            }
        }

        // Animator Controller �̃p�����[�^���Z�b�g����
        if (_anim)
        {
            // ���������̑��x�� Speed �ɃZ�b�g����
            Vector3 velocity = m_rb.velocity;
            velocity.y = 0f;
            _anim.SetFloat("Speed", velocity.magnitude);

            // �n��/�󒆂̏󋵂ɉ����� IsGrounded ���Z�b�g����
            if (m_rb.velocity.y <= 0f && IsGrounded())
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
            m_rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

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

public enum ControlType
{
    /// <summary>����o�C�I�n�U�[�h�̂悤�ȃ��W�R������</summary>
    Turn,
    /// <summary>�J��������Ƃ��������Ɉړ�����</summary>
    Move,
    /// <summary>�J��������Ƃ��������Ɉړ�����B�����]���̍ۂɂ͊��炩�ɉ�]����</summary>
    MoveWithSmoothTurn,
}