using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallExplosion : MonoBehaviour
{
    public List<Rigidbody> _rigid = new List<Rigidbody>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _rigid.Add(transform.GetChild(i).GetComponent<Rigidbody>());
            _rigid[i].isKinematic = true;
            //_rigid[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _rigid[i].isKinematic = false;
            _rigid[i].AddExplosionForce(45f, transform.position - new Vector3(0, -2, 0), 2f, 19, ForceMode.Impulse);
        }
    }


}
