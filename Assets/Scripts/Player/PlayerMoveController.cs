using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
public class PlayerMoveController : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float _movingSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float _turnSpeed = 3f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float _jumpPower = 5f;
    /// <summary>接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float _isGroundedLength = 1.1f;
    /// <summary>キャラクターの Animator</summary>
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
                // 同期元（自分で操作して動かす）オブジェクトの場合のみ Rigidbody, Animator を使う
                _rb = GetComponent<Rigidbody>();
                _anim = GetComponent<Animator>();
            }
        }
    }

    void Update()
    {
        if (!_view.IsMine) return;
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
            dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

            // 入力方向に滑らかに回転させる
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);

            Vector3 velo = dir.normalized * _movingSpeed; // 入力した方向に移動する
            velo.y = _rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            _rb.velocity = velo;   // 計算した速度ベクトルをセットする

        }
        // Animator Controller のパラメータをセットする
        if (_anim)
        {
            // 水平方向の速度を Speed にセットする
            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _anim.SetFloat("Speed", velocity.magnitude);

            // 地上/空中の状況に応じて IsGrounded をセットする
            if (_rb.velocity.y <= 0f && IsGrounded())
            {
                _anim.SetBool("IsGrounded", true);
            }
            else if (!IsGrounded())
            {
                _anim.SetBool("IsGrounded", false);
            }
        }

        // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Debug.Log("Jump");
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

            // Animator Controller のパラメータをセットする
            if (_anim)
            {
                _anim.SetBool("IsGrounded", false);
            }
        }
    }

    /// <summary>
    /// 地面に接触しているか判定する
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;   // start: 体の中心
        Vector3 end = start + Vector3.down * _isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }
}