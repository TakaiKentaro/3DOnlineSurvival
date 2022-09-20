using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class PlayerSpawneManager : MonoBehaviourPunCallbacks // Photon Realtime �p�̃N���X���p������
{
    [SerializeField] string _playerPrefabName = "PlayerPrefab";
    [SerializeField] Transform[] _spawnPositions;

    private void Start()
    {
        SpawnPlayer();
    }

    /// <summary>
    /// �v���C���[�𐶐�����
    /// </summary>
    public void SpawnPlayer()
    {
        // �v���C���[���ǂ��� spawn �����邩���߂�
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;    // ������ ActorNumber ���擾����B�Ȃ� ActorNumber �́u1����v�������ɐU����B
        Debug.Log("My ActorNumber: " + actorNumber);
        Transform spawnPoint = _spawnPositions[actorNumber - 1];

        // �v���C���[�𐶐����A���̃N���C�A���g�Ɠ�������
        GameObject player = PhotonNetwork.Instantiate(_playerPrefabName, spawnPoint.position, Quaternion.identity);

        /* **************************************************
         * ���[���ɎQ�����Ă���l�����ő�ɒB�����畔�������i�Q������ߐ؂�j
         * ��������Ȃ��ƁA�ő�l�����猸�������Ɏ��̃��[�U�[�������Ă��Ă��܂��B
         * ����̃R�[�h�ł̓��[�U�[���ő�l�����猸�����ۂ̒ǉ��������l�����Ă��Ȃ����߁A�ǉ��������������ꍇ�͎�����ύX����K�v������B
         * **************************************************/
        if (actorNumber > PhotonNetwork.CurrentRoom.MaxPlayers - 1)
        {
            Debug.Log("Closing Room");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }


}


