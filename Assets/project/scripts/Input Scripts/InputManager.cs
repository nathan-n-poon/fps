using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controller main;

    Controller[] weapons = new Controller[3];

    private void Start()
    {
        main = GetComponent<Controller>();
        //test
        weapons[0] = GetComponentInChildren<smg>();
        //end test
    }

    public void PassInput(InputData m_InputData)
    {
        main.ReadInput(m_InputData);
        //foreach(Controller weapon in weapons)
        //{
        //    weapon.ReadInput(m_InputData);
        //}
        weapons[0].ReadInput(m_InputData);

    }
}


public struct InputData
{
    public int horizontalMove;
    public int verticalMove;

    public int horizontalLook;
    public int verticalLook;

    public int jumpPressed;
    public int primaryAttackPressed;

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
