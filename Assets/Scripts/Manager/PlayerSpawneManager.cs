using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class PlayerSpawneManager : MonoBehaviourPunCallbacks // Photon Realtime 用のクラスを継承する
{
    [SerializeField] string _playerPrefabName = "PlayerPrefab";
    [SerializeField] Transform[] _spawnPositions;

    private void Start()
    {
        SpawnPlayer();
    }

    /// <summary>
    /// プレイヤーを生成する
    /// </summary>
    public void SpawnPlayer()
    {
        // プレイヤーをどこに spawn させるか決める
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;    // 自分の ActorNumber を取得する。なお ActorNumber は「1から」入室順に振られる。
        Debug.Log("My ActorNumber: " + actorNumber);
        Transform spawnPoint = _spawnPositions[actorNumber - 1];

        // プレイヤーを生成し、他のクライアントと同期する
        GameObject player = PhotonNetwork.Instantiate(_playerPrefabName, spawnPoint.position, Quaternion.identity);

        /* **************************************************
         * ルームに参加している人数が最大に達したら部屋を閉じる（参加を締め切る）
         * 部屋を閉じないと、最大人数から減った時に次のユーザーが入ってきてしまう。
         * 現状のコードではユーザーが最大人数から減った際の追加入室を考慮していないため、追加入室させたい場合は実装を変更する必要がある。
         * **************************************************/
        if (actorNumber > PhotonNetwork.CurrentRoom.MaxPlayers - 1)
        {
            Debug.Log("Closing Room");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }


}


