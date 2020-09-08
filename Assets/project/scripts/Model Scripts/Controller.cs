using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected InputData m_InputData;
    public Camera m_Camera;

    void Awake()
    {
        m_Camera = gameObject.GetComponentInChildren<Camera>();
        Debug.Log(m_Camera);
    }

    public void ReadInput(InputData m_InputData)
    {
        this.m_InputData = m_InputData;
    }

    public void enableCamera()
    {
        m_Camera.enabled = true;
    }

    public void disableCamera()
    {
        Debug.Log(m_Camera);
        m_Camera.enabled = false;
    }

}
