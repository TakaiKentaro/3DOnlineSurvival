using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class SceneManager : MonoBehaviour
{
    [SerializeField, Tooltip("InGameScene–¼")] string _inGameSceneName;

    public void LoadInGameScene()
    {
        PhotonNetwork.LoadLevel(_inGameSceneName);
    }
}
