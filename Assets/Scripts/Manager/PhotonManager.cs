using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

/// <summary>
/// ���r�[����
/// </summary>
public class PhotonManager : MonoBehaviour
{
    /// <summary>
    /// �X�e�[�^�X
    /// </summary>
    public enum PhotonState
    {
        INIT,
        CONNECTED,
        IN_LOBBY,
        READY,
        IN_GAME,
        DISCONNECTED,
        WAITING,
    }

    #region Singleton
    static PhotonManager _instance = null;

    public static bool IsEmpty
    {
        get { return _instance == null; }
    }

    public static PhotonManager Instance
    {
        get
        {
            if (_instance == null)
            {
                System.Type type = typeof(PhotonManager);
                _instance = GameObject.FindObjectOfType(type) as PhotonManager;
            }

            return _instance;
        }
    }
    #endregion

    public delegate void RoomJoinCallback(); //���[���ɓ��������ɌĂ΂�鏈��
    public delegate void GameStartCallback(); //�Q�[�����n�܂������ɌĂ΂�鏈��
    public delegate void GameStopCallback();�@//�Q�[�����~�܂������ɌĂ΂�鏈��
    public delegate void GameEventCallback(int id, int evt); //�C�x���g���������鎞�ɌĂ΂�鏈��
    public delegate void RoomUpdateCallback(List<RoomInfo> roomList); ////���[�����ꗗ���X�V���ꂽ���ɌĂ΂�鏈��

    [SerializeField, Tooltip("�ꏏ�ɒ�����PhotonView")] PhotonView _photonView;
    [SerializeField, Tooltip("1�̃��[���ɉ��l�܂ł�")] int _maxPlayerInRoom = 2;
    [SerializeField, Tooltip("�����I�ɐڑ�")] bool _isAutoConnect = true;
    [SerializeField, Tooltip("�����Ń��[���ɓ���")] bool _isAutoJoin = true;
    [SerializeField, Tooltip("�����N�}�ɂȂ�")] bool _isRankMatching = true;
    [SerializeField, Tooltip("�}�b�`���O�p�����t")] string _onlineKeyword = "";
    [SerializeField, Tooltip("Photon�ŋ��L����郆�[�U���")] UserParam _mySelf = new UserParam();

    [Tooltip("���[�����ꗗ")] List<RoomInfo> _roomList = null;
    [Tooltip("Photon�̐ڑ�State")] PhotonState State;

    /// <summary>
    /// �Q�[�������ǂ���
    /// </summary>
    static public bool IsGameNow
    {
        get
        {
            return _instance && _instance.State == PhotonState.IN_GAME;
        }
    }

    [Tooltip("�l�b�g���[�N�ŋ��L���郆�[�U�[�f�[�^�B�@���r�[�Ń}�b�`���O���Ɏg�p������")] List<UserParam> _player = new List<UserParam>();
    public UserParam GetPlayer(int id)
    {
        return _player[id];
    }

    [Tooltip("��ԊǗ�")] List<int> _playerStatus = new List<int>();

    //�R�[���o�b�N�Q
    RoomJoinCallback _roomJoinCallback;
    GameStartCallback _gameStartCallback;
    GameEventCallback _eventCallback;

#if UNITY_EDITOR
    public RoomUpdateCallback RoomUpdate { get; set; }
#endif
    // Start is called before the first frame update
    void Start()
    {
        //�R�[���o�b�N�͓s���̂����Ƃ���̊֐���ݒ肷��
        _roomJoinCallback = RoomJoin;
        _gameStartCallback = GameStart;
        _eventCallback = GameEvent;

        if (_isAutoConnect)
        {
            Connect();
        }
    }

    /// <summary>
    /// ���[���ɓ������Ƃ��ɌĂ΂�鏈��
    /// </summary>
    void RoomJoin()
    {

    }

    /// <summary>
    /// �Q�[�����n�܂������ɌĂ΂�鏈��
    /// </summary>
    void GameStart()
    {

    }

    /// <summary>
    /// �C�x���g����
    /// </summary>
    /// <param name="id"></param>
    /// <param name="evt"></param>
    void GameEvent(int id, int evt)
    {

    }

    /// <summary>
    /// �ڑ�����
    /// </summary>
    public void Connect()
    {

    }
}
