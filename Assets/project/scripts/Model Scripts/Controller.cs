using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected InputData m_InputData;
    Camera m_Camera;

    void Awake()
    {
        m_Camera = gameObject.GetComponentInChildren<Camera>();
    }

    public void enableCamera()
    {
        m_Camera.enabled = true;
    }

    public void disableCamera()
    {
        m_Camera.enabled = false;
    }

    public void ReadInput(InputData m_InputData)
    {
        this.m_InputData = m_InputData;
    }
}
