using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class KeyBoardTracker : DeviceTracker
{

    void Awake()
    {
        m_InputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_InputData.horizontalMove = (int)Input.GetAxisRaw("Horizontal");
        m_InputData.verticalMove = (int)Input.GetAxisRaw("Vertical");

        m_InputData.horizontalLook = (int)Input.GetAxisRaw("Mouse X");
        m_InputData.verticalLook = (int)Input.GetAxisRaw("Mouse Y");

        newData = (m_InputData.verticalLook | m_InputData.horizontalLook | m_InputData.verticalMove | m_InputData.horizontalMove) != 0 ? true : false;

        if (newData)
        {
            newData = false;
            m_InputManager.PassInput(m_InputData);
            m_InputData.Reset();
        }
    }
}