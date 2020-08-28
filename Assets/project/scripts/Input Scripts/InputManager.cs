using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    public int jumpPressed;

    public void Reset()
    {
        FieldInfo[] members = this.GetType().GetFields();

        foreach (FieldInfo fi in members)
        {
            fi.SetValue(this, 0);
        }
    }

    public bool newData()
    {
        int oorr = 0;
        FieldInfo[] members = this.GetType().GetFields();

        foreach (FieldInfo fi in members)
        {
            oorr |= (int)fi.GetValue(this);
        }

        return oorr != 0 ? true : false;
    }

}
