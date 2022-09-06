using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

/// <summary>
/// ロビー生成
/// </summary>
public class PhotonManager : MonoBehaviour
{
    /// <summary>
    /// ステータス
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

    public delegate void RoomJoinCallback(); //ルームに入った時に呼ばれる処理
    public delegate void GameStartCallback(); //ゲームが始まった時に呼ばれる処理
    public delegate void GameStopCallback();　//ゲームが止まった時に呼ばれる処理
    public delegate void GameEventCallback(int id, int evt); //イベント処理をする時に呼ばれる処理
    public delegate void RoomUpdateCallback(List<RoomInfo> roomList); ////ルーム情報一覧が更新された時に呼ばれる処理

    [SerializeField, Tooltip("一緒に着けるPhotonView")] PhotonView _photonView;
    [SerializeField, Tooltip("1つのルームに何人までか")] int _maxPlayerInRoom = 2;
    [SerializeField, Tooltip("自動的に接続")] bool _isAutoConnect = true;
    [SerializeField, Tooltip("自動でルームに入る")] bool _isAutoJoin = true;
    [SerializeField, Tooltip("ランクマになる")] bool _isRankMatching = true;
    [SerializeField, Tooltip("マッチング用合言葉")] string _onlineKeyword = "";
    [SerializeField, Tooltip("Photonで共有されるユーザ情報")] UserParam _mySelf = new UserParam();

    [Tooltip("ルーム情報一覧")] List<RoomInfo> _roomList = null;
    [Tooltip("Photonの接続State")] PhotonState State;

    /// <summary>
    /// ゲーム中かどうか
    /// </summary>
    static public bool IsGameNow
    {
        get
        {
            return _instance && _instance.State == PhotonState.IN_GAME;
        }
    }

    [Tooltip("ネットワークで共有するユーザーデータ達　ロビーでマッチング時に使用する情報")] List<UserParam> _player = new List<UserParam>();
    public UserParam GetPlayer(int id)
    {
        return _player[id];
    }

    [Tooltip("状態管理")] List<int> _playerStatus = new List<int>();

    //コールバック群
    RoomJoinCallback _roomJoinCallback;
    GameStartCallback _gameStartCallback;
    GameEventCallback _eventCallback;

#if UNITY_EDITOR
    public RoomUpdateCallback RoomUpdate { get; set; }
#endif
    // Start is called before the first frame update
    void Start()
    {
        //コールバックは都合のいいところの関数を設定する
        _roomJoinCallback = RoomJoin;
        _gameStartCallback = GameStart;
        _eventCallback = GameEvent;

        if (_isAutoConnect)
        {
            Connect();
        }
    }

    /// <summary>
    /// ルームに入ったときに呼ばれる処理
    /// </summary>
    void RoomJoin()
    {

    }

    /// <summary>
    /// ゲームが始まった時に呼ばれる処理
    /// </summary>
    void GameStart()
    {

    }

    /// <summary>
    /// イベント処理
    /// </summary>
    /// <param name="id"></param>
    /// <param name="evt"></param>
    void GameEvent(int id, int evt)
    {

    }

    /// <summary>
    /// 接続処理
    /// </summary>
    public void Connect()
    {

    }
}
