using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTest : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(1)|| Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}
