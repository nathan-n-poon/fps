using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected InputData m_InputData;

    public void ReadInput(InputData m_InputData)
    {
        this.m_InputData = m_InputData;
    }
}
