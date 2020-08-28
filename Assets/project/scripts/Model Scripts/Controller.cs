using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Controller : MonoBehaviour
{
    protected bool newData = false;
    public abstract void ReadInput(InputData m_InputData);

    void Awake()
    {

    }
}
