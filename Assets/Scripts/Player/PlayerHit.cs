using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] int _ememyHp = 100;
    [SerializeField] Slider _hpSlider;

    PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
        if (_view)
        {
            if(_view.IsMine)
            {
                _hpSlider.maxValue = _ememyHp;
                _hpSlider.value = _ememyHp;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_view.IsMine) return;
        if(collision.gameObject.CompareTag("Enemy"))
        {
            _ememyHp--;
            _hpSlider.value = _ememyHp;

            if(_ememyHp <= 0)
            {
                Debug.Log("Ž€–S");
                gameObject.SetActive(false);
            }
        }
        
    }
}
