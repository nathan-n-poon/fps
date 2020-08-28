using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingController : Controller
{
    characterMove m_characterMove;
    moveCamera m_moveCamera;
    InputData m_InputData;


    // Start is called before the first frame update
    public override void ReadInput(InputData m_InputData)
    {
        this.m_InputData = m_InputData;
        newData = true;
    }

    private void Awake()
    {
        m_characterMove = GameObject.FindObjectOfType<characterMove>();
        m_moveCamera = GameObject.FindObjectOfType<moveCamera>();
        m_InputData = new InputData(); 
    }

    void Update()
    {
        m_moveCamera.update(m_InputData);
        m_characterMove.update(m_InputData);
        if (newData)
        {
            newData = false;
            m_InputData.Reset();
        }
    }
}
