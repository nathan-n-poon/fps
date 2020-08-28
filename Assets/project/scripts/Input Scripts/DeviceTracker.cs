using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public abstract class DeviceTracker : MonoBehaviour
{
    protected InputManager m_InputManager;
    protected InputData m_InputData;
    protected bool newData;

    private void Awake()
    {
        m_InputManager = GetComponent<InputManager>();
    }
}
