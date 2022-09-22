using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class EventGameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] Slider _hpSlider;
     public int _hp = 0;

    #region Singleton
    static EventGameManager _instance = null;

    public static bool IsEmpty
    {
        get { return _instance == null; }
    }

    public static EventGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                System.Type type = typeof(EventGameManager);
                _instance = GameObject.FindObjectOfType(type) as EventGameManager;
            }

            return _instance;
        }
    }
    #endregion

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {

    }

    void DamageEnemy()
    {

    }
}
