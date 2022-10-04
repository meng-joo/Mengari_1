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
                    _instance = new GameObject() as T;
                    _instance.name = typeof(T).Name;
                    _instance.gameObject.AddComponent<T>(); 
                }
                DontDestroyOnLoad(_instance); 
            }
            return _instance;
        
        }
    }
}
