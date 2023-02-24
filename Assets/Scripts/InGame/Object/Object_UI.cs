using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_UI : MonoBehaviour
{
    [SerializeField] float _hideDistance = 0;
    [SerializeField] Camera _camera;

    private void Update()
    {
        var camera = Vector3.Distance(_camera.transform.position, transform.position);

        if(camera <= _hideDistance)
        {
            Vector3 vector3 = _camera.transform.position - this.transform.position;
            Quaternion quaternion = Quaternion.LookRotation(vector3);
            this.transform.rotation = quaternion;
        }
    }
}
