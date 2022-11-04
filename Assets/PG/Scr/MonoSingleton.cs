using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>() as T; 
                if(_instance == null)
                {
                    var singletonObj = new GameObject();
                    _instance = singletonObj.AddComponent<T>();
                    singletonObj.name = typeof(T).ToString();

                    DontDestroyOnLoad(singletonObj);

                    //_instance = new GameObject() as T;
                    // _instance.name = typeof(T).ToString();
                    // _instance.gameObject.AddComponent<T>(); 
                }
            }
            return _instance;
        
        }
    }
}
