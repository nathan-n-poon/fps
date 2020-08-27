using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Range(0,10)]
    public int axisCount;
    [Range(0,20)]
    public int buttonCount;

    public void PassInput(InputData m_InputData)
    {

    }
}

public struct InputData
{
    public float[] axes;
    public bool[] buttons;

    public InputData(int axisCount, int buttonsCount)
    {
        axes = new float[axisCount];
        buttons = new bool[buttonsCount];
    }

    public void Reset()
    {
        for(int i = 0; i < axes.Length; i++)
        {
            axes[i] = 0;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = false;
        }
    }

}
