using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<Vector3, Vector3> midPos;
    public void RightMousePressed(Vector3 _des, Vector3 _direction)
    {
        if ( midPos != null)
        {
            midPos(_des, _direction);
        }
    }
}
