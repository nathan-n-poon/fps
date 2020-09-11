using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(InputManager))]
public abstract class DeviceTracker : MonoBehaviourPunCallbacks
{
    protected InputManager m_InputManager;
    protected InputData m_InputData;

    private void Awake()
    {
        m_InputManager = GetComponent<InputManager>();
    }
}
