using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Shape
{
    protected string name;
    //걍 버튼으로 만드는게 낫지 않을까?
    public Shape(string _name)
    {
        name = _name;
    }
}