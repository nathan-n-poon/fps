using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Controller m_controller;

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
        verticalMove = 0;
    }

}
