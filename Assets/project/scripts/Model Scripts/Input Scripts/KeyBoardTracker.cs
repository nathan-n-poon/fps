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
        if (!photonView.IsMine) return;
        m_InputData.horizontalMove = (int)getAxisDirection(KeyCode.A, KeyCode.D);
        m_InputData.verticalMove = (int)getAxisDirection(KeyCode.S, KeyCode.W);

        m_InputData.horizontalLook = (int)Input.GetAxisRaw("Mouse X");
        m_InputData.verticalLook = (int)Input.GetAxisRaw("Mouse Y");

        m_InputData.jumpPressed = Input.GetKey(KeyCode.Space) == true ? 1 : 0;

        m_InputData.mouseButton = (int)getAxisDirection(KeyCode.Mouse1, KeyCode.Mouse0);

        m_InputData.interactionButtonPressed = Input.GetKey(KeyCode.F) == true ? 1 : 0;

        m_InputManager.PassInput(m_InputData);
        m_InputData.Reset();

    }

    float getAxisDirection(UnityEngine.KeyCode negative, UnityEngine.KeyCode positive)
    {
        bool neg = Input.GetKey(negative);
        bool pos = Input.GetKey(positive);
        if (pos && neg)
        {
            return 0;
        }
        else if(neg)
        {
            return -1;
        }
        else if(pos)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

//m_InputData.horizontalMove = (int) Input.GetAxisRaw("Horizontal");
//m_InputData.verticalMove = (int) Input.GetAxisRaw("Vertical");

//m_InputData.horizontalLook = (int) Input.GetAxisRaw("Mouse X");
//m_InputData.verticalLook = (int) Input.GetAxisRaw("Mouse Y");

//m_InputData.jumpPressed = Input.GetButtonDown("Jump") == true ? 1 : 0;

//        m_InputData.primaryAttackPressed = Input.GetMouseButton(0) == true ? 1 : 0;

//        m_InputData.interactionButtonPressed = Input.GetKey(KeyCode.F) == true ? 1 : 0;