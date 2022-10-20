using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public SaveManager _saveManager { get; set; }


    private void Awake()
    {
        _saveManager = FindObjectOfType<SaveManager>();
        Debug.Log(_saveManager);
    }



}
