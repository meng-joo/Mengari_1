using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallManager : MonoBehaviour
{
    public static int stageLevel = 1;
    public UnityEvent levelUpEvent;

    public int nowLevel; 

    private void Update()
    {
        nowLevel = stageLevel; 
    }

    [ContextMenu("Up")]
    public void Up()
    {
        stageLevel++; 
    }

    [ContextMenu("Down")]
    public void Down()
    {
        stageLevel--;
    }
}
