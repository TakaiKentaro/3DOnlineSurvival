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
            // �ړI�n�̔ԍ����P�X�V�i�E�ӂ���]���Z�q�ɂ��邱�ƂŖړI�n�����[�v�������j
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _playerPos.Length;
            // �ړI�n�����̏ꏊ�ɐݒ�
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
                Debug.Log("�N���A");
                gameObject.SetActive(false);
            }
        }
        if(other.CompareTag("Guard"))
        {
            // �ړI�n�̔ԍ����P�X�V�i�E�ӂ���]���Z�q�ɂ��邱�ƂŖړI�n�����[�v�������j
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _playerPos.Length;
            // �ړI�n�����̏ꏊ�ɐݒ�
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
