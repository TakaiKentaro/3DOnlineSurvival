using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Tool : MonoBehaviour
{
    [SerializeField] int _dmg;

    private string _tagName;

    private void Start()
    {
        _tagName = gameObject.tag;

        //gameObject.SetActive(false);
    }

    public void Use()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");

        if (other.gameObject.GetComponent<Object_Root>())
        {
            Object_Root obj = other.gameObject.GetComponent<Object_Root>();

            obj.OnCollisionDamage(_tagName, _dmg);
        }
    }
}
