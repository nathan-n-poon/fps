using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controller m_controller;

    private void Start()
    {
        m_controller = GetComponent<Controller>();
    }

    public void PassInput(InputData m_InputData)
    {
        m_controller.ReadInput(m_InputData);

    }
}


public struct InputData
{
    public int horizontalMove;
    public int verticalMove;

    public int horizontalLook;
    public int verticalLook;

    public void Reset()
    {
        horizontalMove = 0;
        verticalMove = 0;
        horizontalLook = 0;
        verticalLook = 0;
    }

}
