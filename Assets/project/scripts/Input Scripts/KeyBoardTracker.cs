using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class KeyBoardTracker : DeviceTracker
{
    public AxisButtons[] axisKeys;
    public KeyCode[] buttonKeys;

    private void Reset()
    {
        m_InputManager = GetComponent<InputManager>();
        axisKeys = new AxisButtons[m_InputManager.axisCount];
        buttonKeys = new KeyCode[m_InputManager.buttonCount];
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < buttonKeys.Length; i++)
        {
            if(Input.GetKey(buttonKeys[i]))
            {
                m_InputData.buttons[i] = true;
                newData = true;
            }
        }

        for (int i = 0; i < axisKeys.Length; i++)
        {
            float val = 0;
            if(Input.GetKey(axisKeys[i].positive))
            {
                val += 1;
                newData = true;
            }
            if (Input.GetKey(axisKeys[i].negative))
            {
                val -= 1;
                newData = true;
            }
            m_InputData.axes[i] = val;

        }

        if(newData)
        {
            m_InputManager.PassInput(m_InputData);
            newData = false;
            m_InputData.Reset();
        }
    }
}

[System.Serializable]
public struct AxisButtons
{
    public KeyCode positive;
    public KeyCode negative;
}
