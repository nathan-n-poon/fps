using System.Collections;
using System.Collections.Generic;
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
