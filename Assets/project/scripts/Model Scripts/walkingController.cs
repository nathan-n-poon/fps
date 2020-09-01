using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class walkingController : Controller
{
    characterMove m_characterMove;
    moveCamera m_moveCamera;
    interactor m_interactor;

    private void Awake()
    {
        m_characterMove = GameObject.FindObjectOfType<characterMove>();
        m_moveCamera = GameObject.FindObjectOfType<moveCamera>();
        m_interactor = GameObject.FindObjectOfType<interactor>();
        m_InputData = new InputData(); 
    }

    void FixedUpdate()
    {
        m_moveCamera.update(m_InputData);
        m_characterMove.update(m_InputData);
        m_interactor.update(m_InputData);
    }
}
