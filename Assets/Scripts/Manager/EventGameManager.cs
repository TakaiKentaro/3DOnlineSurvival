using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
public class EnemyController : MonoBehaviourPunCallbacks
{
    [SerializeField] int _ememyHp = 10;
    [SerializeField] float _enemyMoveSpeed;
    [SerializeField] Transform[] _playerPos;
    [SerializeField] Slider _hpSlider;
    [SerializeField] EventGameManager _enemyManager;

    int _currentWaypointIndex;
    NavMeshAgent _navMeshAgent;
    Rigidbody _rb;
    PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
        if (_view)
        {
            _rb = GetComponent<Rigidbody>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyManager._hp = _ememyHp;
            _navMeshAgent.speed = _enemyMoveSpeed;
        }

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        _playerPos = new Transform[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            _playerPos[i] = gameObjects[i].transform;
        }

        StartCoroutine("MoveDestination");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // 目的地の番号を１更新（右辺を剰余演算子にすることで目的地をループさせれる）
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _playerPos.Length;
            // 目的地を次の場所に設定
            _navMeshAgent.SetDestination(_playerPos[_currentWaypointIndex].position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            _ememyHp--;
            _enemyManager._hp = _ememyHp;
            Debug.Log(_ememyHp);
            if(_ememyHp <= 0)
            {
                Debug.Log("クリア");
                gameObject.SetActive(false);
            }
        }
        if(other.CompareTag("Guard"))
        {
            // 目的地の番号を１更新（右辺を剰余演算子にすることで目的地をループさせれる）
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _playerPos.Length;
            // 目的地を次の場所に設定
            _navMeshAgent.SetDestination(_playerPos[_currentWaypointIndex].position);
        }
    }

    IEnumerator MoveDestination()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            _navMeshAgent.SetDestination(_playerPos[_currentWaypointIndex].position);
        }
    }
}
