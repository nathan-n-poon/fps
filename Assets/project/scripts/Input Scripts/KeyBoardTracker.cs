﻿using System.Collections;
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

        m_InputData.jumpPressed = Input.GetButtonDown("Jump") == true ? 1 : 0;

        m_InputData.primaryAttackPressed = Input.GetMouseButton(0) == true ? 1 : 0;

        m_InputManager.PassInput(m_InputData);
        m_InputData.Reset();
        
    }
}