using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Shape
{
    protected string name;
    //�� ��ư���� ����°� ���� ������?
    public Shape(string _name)
    {
        name = _name;
    }
}