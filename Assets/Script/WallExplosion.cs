using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class WallExplosion : MonoBehaviour
{
    public List<Rigidbody> _rigid = new List<Rigidbody>();
    public List<Vector3> _blocksTransform = new List<Vector3>();
    public Wall _wall;

    private void Awake()
    {
        _wall = transform.parent.GetComponent<Wall>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _rigid.Add(transform.GetChild(i).GetComponent<Rigidbody>());
            _rigid[i].isKinematic = true;

            Vector3 v = transform.GetChild(i).GetComponent<Transform>().position; 
            _blocksTransform.Add(v);
            //_rigid[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _rigid[i].isKinematic = false;
            _rigid[i].AddExplosionForce(25f, transform.position - new Vector3(0, -2, 0), 2f, 12, ForceMode.Impulse);
        }

        StartCoroutine(SetOrigin());
    }

    IEnumerator SetOrigin()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < transform.childCount; i++)
        {
            _rigid[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
            _rigid[i].gameObject.transform.localPosition = _blocksTransform[i];
            _rigid[i].isKinematic = true;
            _rigid[i].gameObject.SetActive(false);
        }

        _wall.Reseting();

        yield break;
    }
}
