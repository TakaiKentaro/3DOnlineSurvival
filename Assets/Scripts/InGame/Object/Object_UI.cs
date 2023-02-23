using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_UI : MonoBehaviour
{
    [SerializeField] float _hideDistance = 0;

    private void Update()
    {
        var camera = Vector3.Distance(Camera.main.transform.position, transform.position);
    }
}
