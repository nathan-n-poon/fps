using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controller main;

    private void Start()
    {
        main = GetComponent<Controller>();
    }

    public void PassInput(InputData m_InputData)
    {
        main.ReadInput(m_InputData);
        //foreach(Controller weapon in weapons)
        //{
        //    weapon.ReadInput(m_InputData);
        //}

    }

    public void setController(Controller newController)
    {
        main = newController;
    }
}


public struct InputData
{
    public int horizontalMove;
    public int verticalMove;

    public int horizontalLook;
    public int verticalLook;

    public int jumpPressed;
    public int mouseButton;
    public int interactionButtonPressed;

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
